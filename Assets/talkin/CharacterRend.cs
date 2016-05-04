using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    MeshRenderer rend;
    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    bool running = false;
    List<List<Animation>> steps;

    bool forceFinish = false;

    public IEnumerator RunRoutine()//incomplete?
    {
        yield return null;
        while (steps.Count > 0)
        {
            foreach (Animation anim in steps[0])
            {
                anim.Start();
            }
            while (steps[0].Count > 0)
            {
                for (int i = 0; i < steps[0].Count; i++)
                {
                    var anim = steps[0][i];
                    if (!anim.animation.MoveNext() || forceFinish) 
                    {
                        anim.Finish();
                        steps[0].RemoveAt(i);
                    }
                }
                yield return null;
            }
            steps.RemoveAt(0);
        }

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

    public class FlipOut : Animation
    {
        public FlipOut(CharacterRend me) : base(me) { }//yeah, this is necessary. sorry.

        public override IEnumerator animate()
        {
            for (int i = 0; i < 360; i++)
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

}


