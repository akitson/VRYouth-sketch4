## Mechanics: Emoji Ball Interaction
- [The Emoji Ball Object](#the-emoji-ball-object)
- [Emoji Grab, Stretch and Tap](#emoji-grab,-stretch-and-tap)
- [Emoji Ball Manager](#emoji-ball-manager)
- [Subword Objects](#subword-objects)
------------
#### The Emoji Ball Object ####

The emoji balls can be found under the `Prefabs` folder in the Editor as the `EmojiBall` prefab. The prefab itself is not set with any texture, but variants with the emoji textures can be found in the `Resources` folder. This is due to the fact that the Photon PUN2 system requires networked prefabs to be placed there.

The emoji ball prefab also contains a rigidbody, but does not have gravity enabled. Also note that the emoji ball also has an additional child object class labeled as `SecondGrabPoint` which is to be used by the stretching mechanic.

Each emoji ball is composed of the following scripts:
- `EmojiBall.cs`: The main emoji ball class that controls the following 3 interactive components.
	- `EmojiGrab.cs`: To move and place emojis in the game space.
	- `EmojiStretch.cs`: To resize the emoji ball using 2 controller anchor points
	- `EmojiTap.cs`: To call the subword spawn function upon controller tap
- `EmojiSubwordTap`: To manage the subwords associated with the emoji ball

The emoji ball essentially switches between the 3 main interactive components with the `EmojiBall.cs`class by enabling/disabling these components. Only one component can be activated at a time. This switching is managed by a global `EmojiBallManager.cs` which is controlled by the `StateManager.cs`

#### Emoji Grab, Stretch and Tap

For emoji **grabbing**, it extends the `XRGrabInteractable.cs` class provided by the XR Interaction Toolkit. The script itself provides events which are to be called if the emoji ball is grabbed, released or placed. Upon placement though, the ball would need to know the `PlacementHint.cs` object it detected. There are also extra RPC methods to synchronize the ball over the network.

For emoji **stretching**, it extends the `XRBaseInteractable.cs` class provided by the XR Interaction Toolkit. Stretching works as follows:
1. Upon grabbing, disable the emoji ball collider. Store the position of the hand grabbing the ball.
2. Enable the `SecondGrabPoint` child object. Store the position of the hand grabbing this object
3. In the `FixedUpdate()` function, measure the distance between these positions to determine the scale of the emoji ball.

The `SecondGrabPoint` collider is slightly larger than the emoji ball when enabled to ensure stretching functionality is gauranteed to work.

For emoji ball **tapping**, it is the simplest of interactive components. It only checks if the object that collided with its trigger is a hand controller.

#### Emoji Ball Manager

This is a global game object, and handles all the emoji ball spawning and synchronization. Per session, only one of these should be active. No singleton system has been implemented to handle multiple emoji ball managers. It contains methods to handle the following:
- Emoji ball spawning and respawning
- Duplicate spawning
- Emoji ball synchronization across client and master
- Emoji ball state changes per emoji ball and all emoji balls

For greater comprehension on what the emoji ball manager does, please seek documentation found in the comments of the `EmojiBallManager.cs`.

#### Subword Objects

The subword prefab can be found under the `Resources` folder as the `SubWord` prefab. The textures on these are to be loaded dynamically via the `MaterialsTable.cs` script, which is to be explained in the next section. 

The only script associated with the subword object is the `SubWordTap.cs`, which follows a similar procedure to the emoji ball tapping mechanic.

The subword objects are managed by the `EmojiSubwordTap.cs` script within the emoji ball. That script contains:
- Spawning of all associated subwords of the emoji ball
- Clearing of all the associated subwords of the emoji ball
- Swapping the emoji ball material with the tapped subword material

## Additional Functionality
- [Materials Table](#materials-table)
- [Network Additions](#network-additions)
- [Controller Additions](#controller-additions)
------------
#### Materials Table

The purpose of the Materials Table is to handle dynamic material changes on the networked objects, without having to send all the changed material bytes over the network.

`MaterialsTable.cs` is run at the very beginning of the game, loading all textures found into 2 dictionaries. One dictionary handles name to material object relationship and another does the reverse. 

To handle material changes over the network with the help of the `MaterialsTable.cs`, simply do the following:
1. Fetch the name of the material that has been set on the client end
2. Do a RPC call over the network with the material name
3. Call the `GetMaterialFromName()` found in the `MaterialsTable.cs` to get the corresponding material

If the object contains the `NetworkAdditions.cs`, the above would not be needed as the `SetMaterial()` handles that functionality.

#### Network Additions
`NetworkAdditions.cs` is an extras class to properly handle the deletion and material changes of networked objects over the Photon PUN2 network. The emoji ball and subword objects use this. This script contains mainly both local side methods and rpc calls to handle deletion, material changes.

#### Controller Additions
`ControllerAdditions.cs` is an extra class for the controllers to expose the primary and secondary button presses. There is a `PrimaryPress` event and a `SecondaryPress` event in which can be listened to if the given controller has any of them pressed. 

Note that there is no back off on these events, that is they do not function as toggle buttons. They can be held and listeners are to be called frequently if it ever happens to do so. This is due to the fact that the invocations are called under the `Update()` function.

