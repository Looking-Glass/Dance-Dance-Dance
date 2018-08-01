I've been working with scans of real-world people and custom models to get them rigged in unity and running in Oliver's squishy dance man scene.  This is a 
stripped-down unity project to use, getting started with adding your own squishy models.

There are two main scenes in the project, in the /Scenes folder.  "Instructable" is a stripped down scene with maximum simplicity.  "Dance Party" is a little more elaborate,
and lets you switch models (up/down arrow) and music/animations (left/right arrow)

I'll put up video documentation of the full process of taking a 3D model and making it an animated dancer in unity on my instructable.
But since you're already here, here's an overview of the process I use (for doing scans of people):

++  Scan a person with a structure sensor.  Pay $6.99 to get the downloadable obj from itseez3d

++  Open up meshlab and reduce the number of polygons from 200,000 (the itseez3d default for a person, which is too high poly for mixamo to handle) down to 35,000 (the highest I can send to mixamo).  Save the new obj and zip the obj, mtl and texture

++  Upload the zip to mixamo -- tell mixamo where the characters chin, ebows, wrists, knees and groin are

++  Download the fbx from mixamo -- this now has the model and a rig.  The texture in the fbx is usually screwed up for no good reason

++  Import the fbx into Unity.  Scale it up to 120 in x,y, and z

++  Also bring in the texture from the original scan -- put the texture into a material and put it onto the model -- now the model looks like the person

++  Add an animation controller to the person

++  Dance the night away!