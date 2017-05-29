David Tsatsoulis

SPACE FORCE

Standalone in the Builds folder. Simply run and enjoy!

Space Force is a traditional Arcade style space shooter game built in Unity for PC.
The Player must endure several waves of enemy forces before facing a terrifying boss, after which the cycle begins again, this time faster.
The game contains 2 modes, Game Mode A (Normal) and Game Mode B (Bullet Hell), where bullets that exit the screen wrap around to the opposite end, adding an additional challenge.
Collect power ups dropped as waves are defeated, and enjoy a score multiplier if an entire wave is destroyed (no ships escape the screen).

Controls
----------
Arrow keys to move up/down/left/right
Space to shoot

There is an executable in the "Builds" folder. The game can also be played in the Unity editor.

Code
--------
The code largely documents itself through variable/function names. The following is a summary of the function of each script.

The following scripts are used for HUD UI functions: BossLifeBar, LifeBarlayer, UIScript.
The following scripts are used for Game Menu functions: SplashScript, MenuControls

GameController is the main script and controls the spawning of waves and the boss, as well as scoring and calling all auxilliary functions that run the game.

All boss behaviour is contained in BossFight, HandBehaviour, HeadBehaviour.

Player movement and controls are in PlayerControl.

All enemy movement and behaviour is in MoveScript, EnemyShot, EnemyShooting, CurvedMovement.

Some background effects are in BackgroundEffects and BaseMove (to move the space station at the start of the game), and startfield functions in StarScript.

Explosion script is used for explosions, and PowerUp for powerups. HellShots is used for bullets fired in Game Mode B (Bullet Hell).

Finally, DestroyByContact is used for contact collision detection, and DestroyByBoundary is for destroying objects that leave the screen.
DestroyByBoundaryHell is similar to DestroyByBoundary but is used to cause bullets to wrap around the screen in Game Mode B (Bullet Hell).
