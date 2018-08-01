I've been working with scans of real-world people and custom models to get them rigged in unity and running in Oliver's squishy dance man scene.  This is a 
stripped-down unity project to use, getting started with adding your own squishy models.

When the project is running, hit 'space' to cycle between characters.

I'll put up video documentation of the full process of taking a 3D model and making it an animated squishy guy in unity over the next couple days, but for now, 
here's an overview of the process I use (for doing scans of people):

++  Scan a person with a structure sensor.  Pay $6.99 to get the downloadable obj from itseez3d

++  Open up meshlab and reduce the number of polygons from 100,000 (the itseez3d default for a person, which is too high poly for mixamo to handle) down to 50,000 (the highest I can send to mixamo).  Save the new obj and zip the obj, mtl and texture

++  Upload the zip to mixamo -- tell mixamo where the characters chin, ebows, wrists, knees and groin are

++  Download the fbx from mixamo -- this now has the model and a rig.  The texture in the fbx is usually screwed up for no good reason

++  Import the fbx into Unity

++  Also bring in the texture from the original scan -- put the texture into a material and put it onto the model -- now the model looks like the person

++  Now set up puppet master -- this makes the person squishable.  The first step is the long one -- you have to create a ragdoll, which involves you manually assigning like 20 joints to a unity script.  

++  Add one more script onto the puppet master model -- the one that allows squishing, and change a couple parameters that adjust the squishability.  

++ Put an animation controller onto the model -- now it's animated.  The easiest way (what I did w/brad) is to use one that I set up ahead of time.  You can also build a new animation controller for a new mixamo animation that comes down with the fbx, but it's just a few more steps of mnnnehhhh

++  Aaaand you're good!