using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JapaneseNato : WordPuzzle
{
    public string lookup(char key)
    {
        switch (key) //this is a hashtable once compiled I promise http://stackoverflow.com/questions/268084/creating-a-constant-dictionary-in-c-sharp
        {
            case 'a': return "アルファ";
            case 'b': return "ブラボー";
            case 'c': return "チャーリー";
            case 'd': return "デルタ";
            case 'e': return "エコー";
            case 'f': return "フォックストロット";
            case 'g': return "ゴルフ";
            case 'h': return "ホテル";
            case 'i': return "インド";
            case 'j': return "ジュリエット";
            case 'k': return "キロ";
            case 'l': return "リマ";
            case 'm': return "マイク";
            case 'n': return "じゅういちがつ";
            case 'o': return "オスカー";
            case 'p': return "パパ";
            case 'q': return "ケベック";
            case 'r': return "ロミオ";
            case 's': return "シエラ";
            case 't': return "タンゴ";
            case 'u': return "均一の";
            case 'v': return "ウイナー";
            case 'w': return "ウィスキー";
            case 'x': return "レントゲン";
            case 'y': return "アメリカ人";
            case 'z': return "ズールーゴ";


            default:
                return key.ToString();
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
