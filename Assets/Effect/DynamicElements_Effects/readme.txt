Hello,
Thank you for purchasing Dynamic Effects.
A few things:
- You are supposed to use these effects by using the "Instantiate" command to create them. Observe the "Raycast" script I use to showcase the effects.
- Always create the whole effect prefab, not just the particle systems inside it.
- If some (mainly the STATUS prefixed ones) disappear too early, you can change the time on the "destroyThisTimed" script
- You can use the "ParticleScale" script to scale individual particle systems (0.5 means 50%, 2 means 200% etc). It is not a full solution though.


If you have any problems or want to share your opinions, you can write to kalamona01@gmail.com.

Cheers,
Gergely Zsolnay


Changelog:

New effects in 1.2:

-	LightningBeam
-	STATUS_Prisoned
-	Starfall + StarfallStorm
-	Fireworks (3 type)
-	RipCombo
-	HitCombo
-	Magma