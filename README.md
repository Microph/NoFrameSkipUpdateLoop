# NoFrameSkipUpdateLoop

Quick demo on how to loop physics and animators update in Unity with adjustable "minimumFramerate" and "simulationTickRate".  
No frame skip more than simulationTickRate/minimumFramerate per simulation tick

Note: I overlooked the Unity's "Maximum Allowed Timestep" in the "Time" setting, will update the repo with this value in consideration later

Example  
- minimumFramerate = 30  
- simulationTickRate = 60  
= Allow at most 1 frame skip per update
