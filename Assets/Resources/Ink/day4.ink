VAR desiredRecipe = ""
VAR name = ""
VAR whileCraftingText = ""
->Encounter_One

==Encounter_One==
~desiredRecipe = "Wound Salve"
~name = "Oma"
[Oma approaches the counter, a visible limp in her step and a twisted slant to her mouth.] 
~name = "player"
Are you alright Oma? 
~name = "Oma"
Oh don't worry about me, hon. I just overdid it yesterday... 
Suppose I finally have to accept I'm not as young as I used to be. 
[She shakes her head.] 
It ain't an easy thing to come to terms with. 
~name = "player"
Is there anything I can do? 
~name = "Oma"
Oh, that'd be lovely, dear. Anything you have that might soothe these aches and pains would be very welcome. 
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
~name = "Oma"
    This is just perfect, thank you hon...
    And if you don't mind me saying, I think you're doing a real good job. 
    I know it ain't an easy task, always working for others, but that's what made your Grandma special. 
    She gave more of herself to this world than we deserved... more than I deserved, surely. 
    She was always there for me, ready to pick me back up and patch me together when I was hurt. 
    If it wasn't for her, I wouldn't be here, and I think 'bout that everyday. 
~name = "player"
    ...You really miss her, don't you?
~name = "Oma"
    I'd miss her more if it wasn't for you. You being here, carrying on her legacy like this? It's more of a comfort then you can know.
~name = "player"
    Well, I...thank you, Oma. I'm trying my best to make her proud.
~name = "Oma" 
    And that's all any of us can do. Well, I best be on my way, but you come down when you've got a day off. I'll show you 'round the garden. 
    ->Encounter_Two
* [Give wrong potion]
    [Oma winces.] No I don't think that's gonna get the job done. Can you mix it up for me again?
    -> Encounter_One.ChoiceTime

==Encounter_Two==
~desiredRecipe = "Dragons Broth"
~name = "Wayfarer"
~whileCraftingText = "Mm.. Smells like home."
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
->ChoiceTime
= ChoiceTime
* [Give Correct Potion]
    ->Encounter_Two.CorrectResponse
* [Give wrong potion]
    -> Encounter_Two.IncorrectResponse
    
= IncorrectResponse
    Hey kid, I know you're still green, but this isn't really what I was looking for...
    Could you try again?
    ->Encounter_Two.ChoiceTime
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