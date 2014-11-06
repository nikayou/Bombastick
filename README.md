# Bombastick

**Bombastick** is a unique mix between the famous arcade game 
[Bomberman](https://en.wikipedia.org/wiki/Bomberman) and 
[Capture the flag](https://en.wikipedia.org/wiki/Capture_the_flag).  
It is currently under developpement, here are some feature not implemented 
yet : 

- 4 players : now, only 2 players are allowed
- joysticks : each player has 5 inputs (directions + bomb)
- level editor : now, maps are randomly generated. There is no guarantee that 
players can reach each other or the flag.

## How to play 

Players can move in 4 directions and drop a bomb.  
![insert screenshot here]()  
Bombs can break some blocks, and kill players. When a player dies, he 
respawns either at its start point, or at the most distant point of the 
action.  
When present, the flag can be picked up simply by moving over it. When killed, 
the flag is dropped on the floor.  
Depending to the mode, the goal is to keep the flag or to kill other people. 

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