# Bombastick

**Bombastick** is a unique mix between the famous arcade game
[Bomberman](https://en.wikipedia.org/wiki/Bomberman) and
[Capture the flag](https://en.wikipedia.org/wiki/Capture_the_flag).  
It is currently under developpement.  

## TODO list

- fix respawn (currently respawning anywhere)
- collisions player-bomb (players are moved when dropping a bomb -> annoying)
- results menu and chaining matches
- real explosion effect (showing exactly what is affected by the explosion)
- test, test, test (with different levels and resolutions)

## How to play

Players can move in 4 directions and drop a bomb.  
![no screenshot yet]()  
Bombs can break some blocks, and kill players. When a player dies, he
respawns at the most distant point of the action.  
When present, the flag can be picked up simply by moving over it. When killed,
the flag is dropped on the floor.  
Depending to the mode, the goal is to keep the flag or to kill other players.

## Modes

### Timed match

When players keep the flag, a timer increases. At the end of the match, the
player with the highest timer wins.

### Target match

The match ends when one player has kept the flag for a given time.  

### Last man match

Usually short, these match claims as winner the player that holds the flag
at the end (or the last player who had it).

### Death match

Here, there is no flag. The goal is simply to kill the most opponent.
