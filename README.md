# D-flector-1
A project where we as a team of five members convert a game concept document into a game during the development of ten weeks. The weeks are split into three phases, two weeks of pre-production, five weeks of production and three weeks of post-production.

The project is an assignment for the course [Game Design 2: Game Development (5SD064)](http://www.uu.se/en/admissions/master/selma/kursplan/?kpid=33553) at [Uppsala University](http://www.uu.se/en).

## Screenshots of the Final Assignment Version

### Main Menu
![Screenshot of the main menu](Documentation/Screenshots/Screenshot_MainMenu.jpg)

### Level Selection
![Screenshot of the level selection menu](Documentation/Screenshots/Screenshot_LevelSelection.jpg)

### Tutorial
![Screenshot of the tutorial](Documentation/Screenshots/Screenshot_Tutorial.jpg)

### Playing
![Screenshot of game play](Documentation/Screenshots/Screenshot_GamePlay.jpg)

### Pause
![Screenshot of the pause menu](Documentation/Screenshots/Screenshot_Pause.jpg)

### Options
![Screenshot of the options menu](Documentation/Screenshots/Screenshot_Options.jpg)


## Conventions

### Naming
#### Files
- Sprites: Spr\_"whos"\_"insert purpose of sprite"
- Animation: An\_"whos"\_"insert action of animation"
- Concept Art: CoArt\_"insert name"
- Code: CS\_"insert what it's for"\_"what it does"
- Prefab: Obj\_"type of object"
- If there several objects of the same type (for example enemies) then specify

#### Unity Tags
- Unity tags should be in CamelCase notation. Capitalize the first letter and separating words. Ex _ExampleTag_

#### Classes
- Class names should be in CamelCase notation. Ex. _ClassName_
- Class properties should be in mixedCase notation. Ex _classProperty_
- Properties that are static should be all capitalized and separated with underscore. Ex _STATIC_PROPERTY_
- Names should be descriptive.
- Names should not be abbreviated.

### Code Standards
- Follow naming conventions.
- Properties should be private to the extent it makes sense.
	- Properties that should be accessible through the editor need to be public.
- Use Unity utility methods and constant variables when possible.
- Remove unused variables and properties.
- Comment only when explaining complex logic.

### Code Review
- Can you understand the changes, does the code do what is intended?
- Does the code follow the "Code standards"?
- Is there any redundant or duplicate code?
- Is the code as modular as possible?
- Can any global variables be replaced?
- Is there any commented out code?
- Do loops have a set length and correct termination conditions?
- Can any logging or debugging code be removed?

### Commit Messages
- Commit messages should isolate one change when possible. If you are adding an _and_ to the message you can probably split the commit up into several commits. Remember that it's better with a lot of smaller commits than with a few large onces.
- Commit messages should explain what and why you have changed something rather than how. The actual content of the commit already explains _how_ so you don't have to do it again.
- Limit the subject line to 50 characters.
- Capitalize the subject line.
- Do not end the subject line with a period.
- Use the imperative mood in the subject line (more information: http://chris.beams.io/posts/git-commit/#imperative).
	- What is within the quotes is the subject line and the prefix is how it should be read, read the link for a detailed description.
	- Example of good: If applied, this commit will "Refactor subsystem X for readability"
	- Example of bad: If applied, this commit will "Fixed bug with Y"
- Example of how commit messages have a tendency to deteriorate, [xkcd: Git commit](https://xkcd.com/1296/).
