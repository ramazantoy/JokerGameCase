# Project Name: Joker Game Case

## Overview

**Joker Game Case** is a game developed for Joker games. 
The player can roll dice, choosing the values of the dice they want to roll. They can rapidly roll a set number of dice automatically, with the dice being randomly selected if rolled quickly. If they roll normally without entering any numbers, the dice will also be randomly selected. During the game, they can reload the map. They can also toggle the main music and sound effects on and off.

## Table of Contents

- [Overview](#overview)
- [Assets Used](#assets-used)
- [Methods Used](#methods-used)
- [Gameplay](#gameplay)
- [Videos](#videos)

## Assets Used

The assets and resources used in the game are as follows:

- **Audio:**
  - Music and sound effects were sourced from opengameart.org.  [https://opengameart.org]
- **Models:**
  - Using free assets, some sourced from Unity Asset Store and some from Sketchfab.

- **Tools and Plugins:**
  - DoTween [https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676]
  - UniTask [https://github.com/Cysharp/UniTask]

## Methods Used

The main methods and techniques used in the development of the game:

- **Game Mechanics:**
  - Physics-based character control: The character moves using a tail; the tail is managed by Unitask, sourced from Unity Asset Store. If there are moves in the tail, the character performs the moves sequentially until there is an empty space.
  - Reward system: When the character makes a move to a location where it should receive a reward, it collects the reward; the reward collection function is located in the BaseTile class.
  - Script communication: Scripts communicate through an event bus.
  - Save system: Game data is managed by the save manager.
  - Audio management: Game sounds are managed by the audio manager.
  - Script modularity: All scripts are independent of each other but communicate through actions.
  - Dice Mechanics : Animation variety: There are 8 different animations for rolling the dice. In cheat rolls, the desired face of the dice is set before rolling, while the remaining faces are randomly selected.

- **Graphics and Animation:**
  - Intro animation: The camera performs an animation when the game is first opened.
  - Animation variety: Different animations are applied to dice rolls to differentiate them from each other.
- **Audio:**
  - Dice rolling: Sound effects play when rolling the dice.
  - UI interaction: Sounds are triggered when interacting with the user interface.
  - Reward collection: Sound effects play when receiving rewards.

## Gameplay

The core gameplay elements are as follows:

- **Controls:**
  - Dice rolling through button: The player initiates a roll through a button.
  - Custom value input: If a value is entered in the top-left corner, the dice roll will be based on those specified values.
  - Toggle for automatic mode: The player can switch to automatic mode using a toggle.
  - Custom roll count: If a value is entered, the dice will roll that many times.
  - Default roll count: If no value is entered, the default is 5 automatic rolls.

## Videos

