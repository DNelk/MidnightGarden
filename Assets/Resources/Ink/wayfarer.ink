VAR desiredRecipe = ""
VAR name = ""
VAR whileCraftingText = ""
->Wayfarer_Two

==Wayfarer_One==
~desiredRecipe = "Warm Healing Tonic"
~name = "Wayfarer"
...
~name = "player"
Hey.. Can I help you?
~name = "Wayfarer"
You're new.
~name = "player"
Yeah, I'm running the shop now. My grandma, she-
~name = "Wayfarer"
[He clicks his tongue]
The less I know, the better, kid. I'm not here to chitchat.
I just got myself out of a scuffle. I could use something to get myself on the mend.
Don't worry about the dosage. I can take a bit of heat.
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    ->Wayfarer_One.CorrectResponse
* [Give wrong potion]
    Come on, you think this is gonna help? I said the hot stuff.
    -> Wayfarer_One.ChoiceTime
= CorrectResponse
    [Sniff]
    That smells about right.
    You might have a knack for this, kid.
    [The corners of his lips start to curl]
    Hah, I remember the first time the old lady-
    ...
    Never mind, I gotta get out of here.
    Until next time.
    ->END
    
==Wayfarer_Two==
~desiredRecipe = "Dragons Broth"
~name = "Wayfarer"
...
~name = "player"
Oh! It's you again.
~name = "Wayfarer"
Howdy, kid.
Looks like you've been doing ok for yourself around here.
~name = "player"
Hey, thanks! It's been a lot of hard work.
~name = "Wayfarer"
...
Can you fix me something to eat?
It's been a few moons since my last good meal.
Something with a bit of starch...
...and spicy, if it's not a big deal for you.
~whileCraftingText = "Mm.. Smells like home"
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    ->Wayfarer_Two.CorrectResponse
* [Give wrong potion]
    -> Wayfarer_Two.IncorrectResponse
    
= IncorrectResponse
    Hey kid, I know you're still green, but this isn't really what I was looking for...
    Could you try again?
    ->Wayfarer_Two.ChoiceTime
= CorrectResponse
    [He inhales deeply]
    ...
    Your grandma made the best soup, didn't she.
    ~name = "player"
    Were you two... close?
    ~name = "Wayfarer"
    Hah. That's the funny part I guess.
    Don't think I ever said more than a word or two to her.
    But I showed up hungry and tired every time I came around to this corner of the world, and she didn't bat an eyelash.
    I guess I came back to...
    ...to say thanks.
    I'll, uh...
    ~name = "player"
    I think I get it.
    ~name = "Wayfarer"
    Be good, kid.
    ->END