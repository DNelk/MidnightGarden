VAR desiredRecipe = ""
VAR name = ""
VAR whileCraftingText = ""
->Encounter_One

==Encounter_One==
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
    ->Encounter_One.CorrectResponse
* [Give wrong potion]
    Come on, you think this is gonna help? I said the hot stuff.
    -> Encounter_One.ChoiceTime
= CorrectResponse
    [Sniff]
    That smells about right.
    You might have a knack for this, kid.
    [The corners of his lips start to curl]
    Hah, I remember the first time the old lady-
    ...
    Never mind, I gotta get out of here.
    Until next time.
    ->Encounter_Two

==Encounter_Two==
~desiredRecipe = "Rust-Away Salve"
~name = "Sir Arium"
HAIL AND WELL MET, SHOPKEEP!!
~name = "player"
Oh! Hey--hi!?
~name = "player"
How can I h--
~name = "Sir Arium"
Where is the old woman?! Sir TERR-33 Arium, Sanctified Defender of These Realms requires her aid once again! 
I can't be seen going into battle with this much oxidation on my chassis.
~name = "player"
Ah, Gran...she isn't with us...But I'm taking over for her now. 
~name = "Sir Arium"
Impossible! The old witch never misses a day! 
If her shop was open when the Battle of Newich came through THIS FIELD, surely she must be willing to take an appointment.
Please. Point me to your employer, youngling.
~name = "player"
Sir. I am my employer. My grandmother has recently passed. I'll prepare your order right away.
~name = "Sir Arium"
...I see.
~whileCraftingText = "[A long, awkward silence hangs in the air.]"
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    You work with your grandmother's diligence, youngling. I can see it in your hands...
    Well...I'll be off now. Fare thee well!
    ->END
* [Give wrong potion]
    I admire your intent, youngling, but I'd prefer something to handle this unsightly rust.
    
    -> Encounter_Two.ChoiceTime