VAR desiredRecipe = ""

->Encounter_One

==Encounter_One==
~desiredRecipe = "The Juice"
this is a test game...
<>yes it is
i want the juice
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    you got it right
    ->Encounter_Two
* [Give wrong potion]
    you fucked up
    -> Encounter_One.ChoiceTime

==Encounter_Two==
~desiredRecipe = "Just Kale Please"
here's the second encounter
now i want just kale please
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    you got it right
    ->END
* [Give wrong potion]
    you fucked up
    -> Encounter_Two.ChoiceTime
