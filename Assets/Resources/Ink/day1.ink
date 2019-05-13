VAR desiredRecipe = ""
VAR name = ""
VAR whileCraftingText = ""
->Encounter_One

==Encounter_One==
~desiredRecipe = "Gill Serum"
~name = "TestName1"
hi i'm an npc
~name = "player"
hey i'm the player
~name = "TestName1"
this is a test game...
<>yes it is
i want gill serum
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    you got it right
    ->Encounter_Two
* [Give wrong potion]
    you fucked up
    -> Encounter_One.ChoiceTime

==Encounter_Two==
~desiredRecipe = "Shake Light"
~name = "TestName2"
~whileCraftingText = "Nice Lookin Potions"
here's the second encounter
now i want a shake light
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    you got it right
    ->END
* [Give wrong potion]
    you fucked up
    -> Encounter_Two.ChoiceTime
