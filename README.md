# Hexagonal tiles for maps in the Enity engine.

Map generation component for Unity, which utilizes several layers of perlin noise to generate biomes and terrain.

![alt text](http://i67.tinypic.com/2m6lqnp.png)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development, testing and demonstration purposes

 * **Step 1 :** [Download the project](https://github.com/r59q/Hexagonal_Tile-Map--Unity/releases)

 * **Step 2 :** Unpack the Unity project onto your computer, any location will do.

 * **Step 3 :** Go to the assets/scenes folder and open demo.scene
 
 *Now the project should be open, and you should be able to press play and run the project from within Unity.*

* **Step 4:** [Read the wiki](https://github.com/r59q/Hexagonal_Tile-Map--Unity/wiki)

### Prerequisites

    You will need Unity 2017

    **How to install**
     * Go to unity's website
     * Download unity installer
     * Install

## Installing

These instructions detail how to set up a copy of the minimal version (Only core C# scripts, no tile assets or scenes).

It is recommended if you are already working on a project or are already experienced with this component.

&nbsp;

Let's get started.

 * **Step 1 :** [Download](https://github.com/r59q/Hexagonal_Tile-Map--Unity/releases) the minimal version of the project, which is in the release description.
 
 * **Step 2 :** Import the scripts into your project by opening the .unitypackage file whilst your unity project is open.
 
    * *If you do not already have a GameManager object*
 
    * **Step 3 :** Create an empty object, and name it.
 
 * **Step 4 :** Add script 'PerlinGenerator' from the .unitypackage to your GameManager object.
 
    * *If you do not already have a GameManager script on your GameManager object
 
    * **Step 5 :** Add a new c# script to your GameManager object and name it anything.
 
 * **Step 6 :** Add the following code to your start method.
 
```csharp
GetComponent<MapGenerator>().Build(); // PerlinGenerator is an extension of Mapgenerator
```

* **Step 7 :** Tweak the settings on the PerlinGenerator component, that you added to your GameManager object, to suit your needs.

Visit the [wiki](https://github.com/r59q/Hexagonal_Tile-Map--Unity/wiki) for more information.

&nbsp;

- - - -

&nbsp;


## Versioning

For the most stable versions available, see the [tags on this repository](https://github.com/r59q/Hexagonal_Tile-Map--Unity/tags). 

## Authors

* **A. Malthe Henriksen** - [r59q](https://github.com/r59q)

See also the list of [contributors](https://github.com/r59q/Hexagonal_Tile-Map--Unity/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/r59q/Hexagonal_Tile-Map--Unity/blob/master/LICENSE) file for details
