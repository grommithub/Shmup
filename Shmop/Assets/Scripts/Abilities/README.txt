A short explanation of how does the system work:

*At first, there are two core files that are being used with any ability: Ability.cs and AbilityCooldown.cs.
	
	1. Ability.cs is an abstract Scriptable Object that describes basic variables that every ability has (name, sprite of an icon, 
	sound effect when triggered and cooldown period) and also two basic methods for each ability: Initialize() and TriggerAbility(). The
	names speak for themselves. The first method sets all references, get required components and assigns values to the variables. 
	The second contains the code that should be executed when ability is called. But, for the sake of effeciency and conveniency, it usually
	contains only methods from another type of script, which is a bit of a different story.
	
	2. AbilityCooldown.cs is MonoBehaviour class that contains all the logic of the system. Basically, it does all the job (launches ability,
	starts cooldown, depicts cd progress in the HUD etc.). It is attached to the "AbilityIcon" in the scene and contains the 
	ability prefab, info about required input and player gameobject. Currently all it needs to work properly when creating a 
	new ability is choosing an input key and correct ability prefab.

	**Player or any other GameObject that ability is supposed to interact with (eg: a gunpoint)

*When creating a new ability, two files have to be created: %Ability_name%Ability.cs and %Ability_name%Trigger.cs 

	1. %Ability_name%Ability.cs is a derived class that inherits from Ability.cs. It contains the description of Initialize() and 
	TriggerAbility() methods, as well as all aditional variables required for proper work of this ability. It also has to include a 
	variable that gets reference out of %Ability_name%Trigger.cs

	2. %Ability_name%Trigger.cs is a MonoBehaviour class that contains all the logic of ability: what it actually does when it is called. 
	It is attached to the player object as a component and contains the methods that should execute the code of ability. Theoretically, 
	TriggerAbility() method should be described using only functions from this script.
	
*Steps for creating new ability: 
	
	1. Write %Ability_name%Trigger.cs
	2. Attach this script to the player or whatever object is gonna be used (target object)
	2. Write %Ability_name%Ability.cs
	3. Create a new ability asset out of it
	4. Drag an "AbilityIcon" prefab into the UI gameobject in the scene, tweak the parameters and settings as you wish.
	5. Here, in the "Ability Cooldown script", choose the gameobject you want to attach script to, activation key and new abillity prefab.
	