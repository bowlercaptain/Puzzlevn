Hop: makes the character hop in place. Height and speed are controlled by CharacterRend.Hop.HOPDURATION and .HOPHEIGHT

usage: \<\<Hop Becky\>\>


HopMulti: it does what you think. Takes an int of how many times to hop.

usage: \<\<HopMulti Becky 15\>\>            <sup><sub>damn, becky, chill.</sup></sub>


Hide: Fades the character to clear over CharacterRend.Hide.HIDETIME seconds.

usage: \<\<Hide Becky\>\>


HideNow: makes the character clear instantly.

usage: \<\<HideNow Becky\>\>


FadeUp: fades the character two white over 100 frames. That should *definitely* change if we're going to use this in conjunction with hide.

usage: \<\<FadeUp Becky\>\>


FadeDown: Fade to grey over 100 frames. Given that three animations are basically identical besides a name and a color, I think I will mix them all together at some point. We may want slow/fast alternatives and shorthand for colors (up/down/hide for white/grey/clear)

usage: \<\<FadeDown Becky\>\>


Enter: Character slides onto the screen from a direction. takes up/down/left/right for dir. Speed and distance are currently hardcoded. Sorry.

usage: \<\<Enter Becky Right\>\>            <sup><sub>YooooooOooOOOOOOOO~!!!!</sup></sub>       


HideText: hides the dialogue and nametag boxes as if it were a line of dialogue. Useful in conjunction with place background for showing full-screen images.

usage: \<\<HideText\>\>


PlaySound: Plays a sound effect from Resources/Sounds. Ignores file extensions and only uses name.

usage: \<\<PlaySound Boing\>\>
Plays Assets/Resources/Boing.ogg (or .wav or Whatever) once


PlayUniqueLooping: Plays a sound(or music) repeatedly, but takes a tag so that sound can be replaced or cut off. this might want to be shorter. PlayBGM?

usage: \<\<PlayUniqueLooping MusicName bgm>>
plays Assets/Resources/MusicName.aiff until further notice.


Place Character: Puts a character into a slot onscreen and can optionally hide them in the sky for later position-animating. This will have the character show their "default" emotion.

usage: \<\<place right Becky hide\>\>
puts the character "Becky" in the slot "right" and hides her.
usage: \<\<place center AmazingArt\>\>
The hide/show argument is optional. The slot names and values are in PortraitDisplay.slotList, but at time of writing are right/center/left/background


Emote: Makes a character show a specific emotion. The emotions are pieces of art in Resources/Characters/CharacterName/EmotionName.image

usage: \<\<emote Becky smug\>\>


----


FlipOut: Wheee

usage: \<\<FlipOut Becky\>\>


MoveSlot: This one isn't really done right now. I think it teleports the character into place in a new slot onscreen but ¯\\_(ツ)_/¯

usage: \<\<MoveSlot Becky SlotName\>\>


CheatyFace: I thought I could be clever and take a variable number of arguments with a params list, but nope, Yarnie don't play that

usage: you can't.