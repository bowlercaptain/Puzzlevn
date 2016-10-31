using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class TextRenderer : MonoBehaviour
{
    public Text textbox;

    public IEnumerator renderText(string text, ICollection<string> tags)
    {
        string outPutText = "";
        List<string> modes = new List<string>();
        if (tags != null)
        {
            if (tags.Contains("italic"))
            {
                outPutText += "<i>";
                modes.Add("</i>");
                //this should maybe be a short list of rich text types with regexes for their beginnings and strings for their ends. could also do some careful parsing to remove the arguments and autogenerate a closing tag. Hardcoded for now.
            }
            if (tags.Contains("bold"))
            {
                outPutText += "<b>";
                modes.Add("</b>");
                //this should maybe be a short list of rich text types with regexes for their beginnings and strings for their ends. could also do some careful parsing to remove the arguments and autogenerate a closing tag. Hardcoded for now.
            }
        }
        bool skipToEnd = false;
        for (int i = 0; i < text.Length; i++)//todo: sloooow down (waitforseconds or catch up to a timestamp)
        {
            if (!skipToEnd)
            {
                textbox.text = outPutText + string.Join("", modes.ToArray());//copypasta:A
                yield return null;

                if (ThisIsUI.CheckContinue()) { skipToEnd = true; }//after YRN to let input from last frame filter out
            }
            
            
                if (text[i] == '<' && text.Length >= i + 2 && text[i + 2] == '>')
                {
                    outPutText += text.Substring(i, 3);
                    modes.Add("</" + text[i + 1] + ">");
                    i += 2;
                }
                else if (text[i] == '<' && text.Length >= i + 3 && text[i+1]=='/' && text[i + 3] == '>')
                {
                    outPutText += text.Substring(i, 4);
                    Debug.Assert(modes.Remove(text.Substring(i, 4)));
                    i += 3;
                }
                else
                {
                    outPutText += text[i];
                }
            



            //Todo: play voice letter clip.
                    }
        textbox.text = outPutText + string.Join("", modes.ToArray());//copypasta:A
        yield break;
    }


    public static string ChompLeadingSpace(string toChomp)
    {
        if (toChomp.Length > 0 && toChomp[0] == ' ') { return toChomp.Substring(1); }
        return toChomp;
    }
}
