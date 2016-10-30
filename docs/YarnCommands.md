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


FlipOut: Wheee

usage: \<\<FlipOut Becky\>\>


MoveSlot: This one isn't really done right now. I think it teleports the character into place in a new slot onscreen but ¯\_(ツ)_/¯

usage: \<\<MoveSlot Becky SlotName\>\>