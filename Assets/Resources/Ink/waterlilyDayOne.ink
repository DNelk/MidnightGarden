VAR desiredRecipe = ""
VAR name = ""
VAR whileCraftingText = ""
->Encounter_One

==Encounter_One==
~desiredRecipe = "Sweet Healing Tonic"
~name = "Waterlily"
~whileCraftingText = "I hope Fern hasn't wandered off."
[The stranger seems skittish and startled, like a nervous deer.] 
Oh – hello! 
I - I heard about Grandma's passing. What a terrible thing... are you the grandchild we've all been hearing about? It must be hard, reopening the shop all by yourself. 
~name = "player"
Yeah, it's been a bit tough to set up without her guidance. Things are going okay so far, though.
~name = "Waterlily"
Ah, that's great to hear! Um, well, I don't need anything too complex. Oh – where are my manners? My name is Waterlily. 
I'm from the pond just north of here.
~name = "player"
Nice to meet you, Waterlily.
~name = "Waterlily"
You as well! 
Could you make me something ... sweet and healy? My little sister has injured herself quite badly, but she refuses to take any of our medicine because it's too bitter. It's made her quite cross.
[She sighs.]
Grandma used to make this pink thing that Fern loved. 
~name = "player"
I've got some big shoes to fill, huh?
~name = "Waterlily"
Oh! I'm so sorry, I didn't mean to imply anything. It's only that... we all loved her. And I've no doubt you did, as well. She tolerated every haggard visit from me to order that pink healing potion whenever Fern hurt herself adventuring. 
Some of which was on purpose, I will add! The little nuisance went to great lengths to get that potion.
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    [Waterlily's face lights up.] 
    This is it! Thank you very much! I thought I'd never hear the end of Fern's complaints. 
    [She pauses, then gives you a sincere nod.] 
    You're doing wonderfully, by the way. Your grandmother would be proud of you.
    ->Encounter_Two
* [Give wrong potion]
    [She examines the potion carefully, evidently trying to hide disappointment with a polite smile.] 
    Ah... this isn't it, I'm sorry to say. B-But don't worry! I've got lots of time for you to try again.
    -> Encounter_One.ChoiceTime

==Encounter_Two==
~desiredRecipe = "uhh"
~name = "TestName2"
~whileCraftingText = "Nice Lookin Potions"
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
