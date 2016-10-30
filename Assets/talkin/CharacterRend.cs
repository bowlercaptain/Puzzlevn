using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(MeshRenderer))]
public class CharacterRend : MonoBehaviour
{

    public Texture texture
    {
        get { return rend.material.mainTexture; }
        set { rend.material.mainTexture = value; }
    }

    public Color color
    {
        get { return rend.material.color; }
        set { rend.material.color = value; }
    }

    public PortraitDisplay.Slot targetSlot;

    MeshRenderer rend;
    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    List<Dictionary<string, Animation>> steps = new List<Dictionary<string, Animation>>();

    bool forceFinish = false;

    public void AddUnique(Animation toAdd)
    {
        Add(toAdd.GetHashCode().ToString(), toAdd);
    }

    public void Add(string tag, Animation toAdd)
    {
        AddWithDelay(tag, toAdd, 0);
    }

    public void AddWithDelay(string tag, Animation toAdd, int delay)
    {
        toAdd.animation = WaitOneFrameThenRun(toAdd.Start);
        while (steps.Count <= delay) { steps.Add(new Dictionary<string, Animation>()); }
        steps[delay][tag] = toAdd;//overwrite same tag
        if (!isRunning) { StartCoroutine(RunRoutine()); }
    }

    private IEnumerator WaitOneFrameThenRun(Action toRun)
    {
        yield return null;
        toRun();
    }

    private List<string> toRemove = new List<string>();

    bool isRunning = false;
    public IEnumerator RunRoutine()//incomplete?
    {
        isRunning = true;
        while (steps.Count > 0)
        {
            foreach (Animation anim in steps[0].Values)
            {
                anim.Start();
            }
            while (steps[0].Count > 0)
            {
                foreach (string tag in steps[0].Keys)
                {
                    var anim = steps[0][tag];
                    if (!anim.animation.MoveNext() || forceFinish)
                    {
                        anim.Finish();
                        toRemove.Add(tag);
                    }
                }
                if (toRemove.Count > 0)
                {
                    foreach (string tag in toRemove)
                    {
                        steps[0].Remove(tag);
                    }
                    toRemove = new List<string>();
                }

                yield return null;
            }
            steps.RemoveAt(0);
        }
        isRunning = false;
        yield return null;
    }

    public class Animation
    {
        protected CharacterRend me;
        public IEnumerator animation;
        public void Start()
        {
            animation = animate();
        }
        public Animation(CharacterRend me)
        {
            this.me = me;
        }
        public virtual IEnumerator animate() { yield break; }//can be interrupted at any time, must not leave the
        public virtual void Finish() { }//Set the renderer into a "Finished" state. This may be called, and the Animation interrupted and inclomplete, at any frame (including the 0th).
    }

    [YarnCommand("FlipOut")]
    public void FlipOutA()
    {
        Add("Rotation", new FlipOut(this));
    }

    public class FlipOut : Animation
    {
        public FlipOut(CharacterRend me) : base(me) { }//yeah, this is necessary. sorry.

        public override IEnumerator animate()
        {
            for (int i = 0; i < 360; i += 4)
            {
                me.transform.rotation = Quaternion.Euler(0, 0, i);
                yield return null;
            }
        }

        public override void Finish()
        {
            me.transform.rotation = Quaternion.identity;
        }
    }

    [YarnCommand("Enter")]
    public void EnterA(string dir)
    {
        Add("Entry", new Enter(this, dir));
    }

    public class Enter : Animation
    {
        const float OFFSETMAGNITUDE = 10f;

        string dir;
        public Enter(CharacterRend me, string dir) : base(me) { this.dir = dir; }

        public override IEnumerator animate()
        {
            Dictionary<string, Vector3> offsetdirs = new Dictionary<string, Vector3>() {
            {"up",Vector3.up },
            {"down",Vector3.down },
            {"left",Vector3.left },
            {"right",Vector3.right }
            };

            Vector3 offSetDir;
            Debug.Assert(offsetdirs.TryGetValue(dir, out offSetDir),"Enter direction not valid: "+dir);
            offsetdirs.TryGetValue(dir, out offSetDir);


            for (float i = (float)Math.PI / 2f; i >= 0; i -= 1 / 60f)
            {
                me.transform.position = me.targetSlot.position + offSetDir * OFFSETMAGNITUDE * (1 - Mathf.Cos(i));
                yield return null;
            }
        }

        public override void Finish()
        {
            me.transform.position = me.targetSlot.position;
        }
    }

    const float highlightScaleFactor = 1.3f;
    const float highlightFadeTime = .366f;
    [YarnCommand("FadeUp")]
    public void FadeUpA()
    {
        Add("Color", new FadeScale(this, this.targetSlot.scale * highlightScaleFactor));
        Add("Size", new FadeColor(this, Color.white));
    }

    [YarnCommand("FadeDown")]
    public void FadeDownA()
    {
        Add("Color", new FadeScale(this, this.targetSlot.scale));
        Add("Size", new FadeColor(this, Color.grey));
    }

    public class FadeColor : Animation
    {
        public FadeColor(CharacterRend me, Color targetColor) : base(me) { this.targetColor = targetColor; }
        Color targetColor;
        public override IEnumerator animate()
        {
            Color startColor = me.color;
            for (float i = 0; i < 1f; i += Time.deltaTime / highlightFadeTime)
            {
                Debug.Log("Fading in");
                me.color = Color.Lerp(startColor, targetColor, i);
                yield return null;
            }
        }

        public override void Finish()
        {
            me.color = targetColor;
        }
    }



    public class FadeScale : Animation
    {
        public FadeScale(CharacterRend me, Vector3 targetScale) : base(me) { this.targetScale = targetScale; }
        Vector3 targetScale;
        public override IEnumerator animate()
        {
            Vector3 startScale = me.transform.localScale;
            for (float i = 0; i < 1f; i += Time.deltaTime / highlightFadeTime)
            {
                me.transform.localScale = Vector3.Lerp(startScale, targetScale, i);
                yield return null;
            }
        }

        public override void Finish()
        {
            me.transform.localScale = targetScale;
        }
    }



    [YarnCommand("Hide")]
    public void HideA()
    {
        Add("Color", new Hide(this));
    }

    public class Hide : Animation
    {
        const float HIDETIME = 1f;
        public Hide(CharacterRend me) : base(me) { }

        public override IEnumerator animate()
        {
            Color startColor = me.color;
            for (float time = 0f; time < HIDETIME; time += Time.deltaTime)
            {
                Debug.Log("Fading out");
                me.color = Color.Lerp(startColor, Color.clear, time / HIDETIME);
                yield return null;
            }
        }

        public override void Finish()
        {
            me.color = Color.clear;
        }
    }

    [YarnCommand("HideNow")]
    public void HideNowA()
    {
        Add("Color", new HideNow(this));
    }

    public class HideNow : Animation
    {
        public HideNow(CharacterRend me) : base(me) { }

        public override IEnumerator animate()
        {
            yield break;
        }

        public override void Finish()
        {
            me.color = Color.clear;
        }
    }

    //Todo: figure out how to get slot position from name.
    [YarnCommand("MoveSlot")]
    public void MoveSlotA(string slotName)
    {
        Add("Slot", new MoveSlot(this, slotName));
    }

    public class MoveSlot : Animation
    {
        public MoveSlot(CharacterRend me, string slotName) : base(me)
        {
            destSlot = slotName;
        }

        private string destSlot;
        public override IEnumerator animate()
        {
            yield return null;
            //TODO: move towards slot location.
        }

        public override void Finish()
        {
            //FIXME: don't find that's a scrub move make a static reference in the class to the current instance
            FindObjectOfType<PortraitDisplay>().PlaceCharacter(destSlot, me);
            //portraitdisplay.setCharacter(CharacterRend me, slotIndexFromName(slotName));
        }
    }



    [YarnCommand("Hop")]
    public void HopA()
    {
        HopM("1");
    }

    [YarnCommand("HopMulti")]
    public void HopM(string times)
    {
#if UNITY_EDITOR
        int outt;
        Debug.Assert(int.TryParse(times, out outt));
#endif
        Add("Hop", new Hop(this, int.Parse(times)));
    }


    public class Hop : Animation
    {
        const float HOPDURATION = .25f;
        const float HOPHEIGHT = 30f;
        int times;
        public Hop(CharacterRend me, int times) : base(me) { this.times = times; }

        public override IEnumerator animate()
        {
            for (int x = 0; x < times; x++)
            {
                for (float i = 0; i <= 1; i += Time.deltaTime / HOPDURATION)
                {
                    me.transform.position = me.targetSlot.position + HOPHEIGHT * Vector3.up * (i - i * i);
                    yield return null;
                }
            }
        }

        public override void Finish()
        {
            me.transform.position = me.targetSlot.position;
        }
    }


    [YarnCommand("CheatyFace")]
    public void CheatA(params string[] lolll)
    {
        foreach (string lol in lolll)//this doesn't work boooooo
        {
            Debug.Log(lol);
        }
    }
}


