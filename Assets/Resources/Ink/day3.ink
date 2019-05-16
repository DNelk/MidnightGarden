VAR desiredRecipe = ""
VAR name = ""
VAR whileCraftingText = ""
->Encounter_One

==Encounter_One==
~desiredRecipe = "Robot Ramen"
~name = "Sir Arium"
SALUTATIONS, YOUNGLING!!
~name = "player"
Ah! Sir Arium, you have GOT to work on your intro.
~name = "Sir Arium"
Ahem! *WELL MET YOUNGLING!*
~name = "player"
Thank you. What can I get for you today?
~name = "Sir Arium"
 Well, shopkeep. I've returned to extend an apology. The way that I spoke was not very...chivalrous of me.
~name = "player"
Oh...you didn't have to--I mean, it wasn't that big a deal!
~name = "Sir Arium"
Oh pish-posh! This is the former dwellings of a legend, youngling. Surely you must know about your grandmother's accomplishments.
~name = "player"
I...didn't know her too well, actually. 
I just remember her giving me treats as a child and journeying on for months, even years at a time. 
I didn't even know she'd remember me until I saw her will. You're a knight, and you seem to look up to her. Was she your superior?
~name = "Sir Arium"
Superior? Hilarious. 
She wouldn't touch the military with a 10-foot lance! 
She was, however, a guiding beacon for any beleagured soldier, offering healing salves and meals for the wounded. 
All sides of the Highlands Mechanized Conflicts would gather at this shop and enjoy a noodle dish prepared with the most aromatic motor oil broth.
My taste sensors warm at the mere memory of the flavor.
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    By the Highlands, you've done it! Your grandmother would be so proud...
    I don't know what the outcome of this next battle will be, but I will go forth with with my emotional sensors set to "warm".
    Fare thee well, youngling!
    ->Encounter_Two
* [Give wrong potion]
    I appreciate this gift, youngling. Would you happen to have the ingredients to this noodle soup on hand?
    -> Encounter_One.ChoiceTime

==Encounter_Two==
~desiredRecipe = "Spiced Siren Cider"
~name = "Waterlily"
~whileCraftingText = "[Waterlily stifles a yawn behind her hand.]"
Hello again!
~name = "player"
How's it going, Waterlily?
~name = "Waterlily"
[She gives you a bright smile, but you notice she looks a bit frazzled.] 
I'm well, thank you. And yourself?
~name = "player"
I'm doing okay. What would you like?
~name = "Waterlily"
Ah! Could you make something warm to drink? It's been a bit cold around the pond, and Fern's come down with the gray.
[Your confusion registers, and she seems to realize you don't understand what she means.]
Oh – she's ill. I believe you and Grandma would call it a cold. Her gills clogged up, and she's all chilly. 
~name = "player"
Sure thing. Are you okay, Waterlily? You look a little tired.
~name = "Waterlily"
Oh, no, don't worry! It's just one of those days. 
Fern is acting out a lot lately, and now she's ill! Managing that on top of taking care of her... it's been a little hectic.
~name = "player"
Can't your family help out with that?
~name = "Waterlily"
[Waterlily averts her gaze, falling silent.]
~name = "player"
Ah – I'm sorry if I overstepped.
~name = "Waterlily"
No, that's alright. In truth, Fern and I live on our own. 
We don't have any other family. There are plenty of fellow nymphs in our community, but... most of them live in rivers and ponds at least a day's travel away.
That's why Grandma... took us under her wing, so to speak.
~name = "player"
She raised you?
~name = "Waterlily"
[She nods, her smile a little sad.] 
She brought us food, wove us clothes, made sure we had everything we needed. She even let us stay in the back room of the shop during the war, to keep us safe. She – 
She was my hero. 
~name = "player"
It must be tough to take care of Fern by yourself now.
~name = "Waterlily"
It can be, yes... being the strong one is exhausting sometimes. You don't – you don't really have time to grieve.
But i-it's alright, really. I love my sister. And I know Grandma is watching over us still, from – from wherever she may be.
~name = "player"
[You nod, returning the sad smile. Better get started on that warm drink for Fern.]
~name = "Waterlily"
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    This is perfect. It even has gill serum for clearing out her waterways! Thank you again!
    ->END
* [Give wrong potion]
    Mm... I-I'm sorry, this isn't quite what I was looking for.
    -> Encounter_Two.ChoiceTime