# Saver_Demo_Unity3D
This is a demonstration of my Unity3D and C# skills. (Created at 19 August 2021) \
\
Linkedin: https://www.linkedin.com/in/mehmet-kutlu-polat-aa60a0211/
\
NOTES:\
SpawnSaver class is tightly coupled with SpawnUnits and SpawnUnits class is tightly coupled with SetSpawnableArea.\
Because of that, the constructor execution order will be SetSpawnableArea() > SpawnUnits() > SpawnSaver()\
