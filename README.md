# Shooter
A project where we as a team of five members convert a game concept document into a game during the development of ten weeks. The weeks are split into three phases, two weeks of pre-production, five weeks of production and three weeks of post-production.

The project is an assignment for the course [Game Design 2: Game Development (5SD064)](http://www.uu.se/en/admissions/master/selma/kursplan/?kpid=33553) at [Uppsala University](http://www.uu.se/en).

## Conventions

### Naming
#### Files
- CamelCase, capitalize the first letter and separating words. Ex "FileName.txt"

#### Classes
- CamelCase, the class name. Ex. "NewClassName"
- camelCase, property names. Ex "newProperty"
- Properties that are static should be all capitalized and seprated with underscore "_". Ex "STATIC_PROPERTY"
- Names should be descriptive.
- Names shouldn't be abbriviated.

### Code standards
- Follow naming conventions.
- Properties should be private to the extent it makes sense.
	- Properties that should be accessible through the editor need to be public.
- Use Unity util methods and const variables when possible.
- Remove unused variables and properties.
- Comment only when explaining complex logic.

### Code review
- Can you understand the changes, does the code do what is intended?
- Does the code follow the "Code standards"?
- Is there any redundant or duplicate code?
- Is the code as modular as possible?
- Can any global variables be replaced?
- Is there any commented out code?
- Do loops have a set length and correct termination conditions?
- Can any logging or debugging code be removed?

### Commit standards
- A commit should isolate ONE change when possible! It is better with a lot of smaller commits than a few large ones, because nobody likes huge merge conflicts!
- Commit messsages should describe what you have changed. If you need to describe more then one change or find yourself using an "and" you can probably break your commit into smaller parts.
- Limit the subject line to 50 characters
- Capitalize the subject line
- Do not end the subject line with a period
- Use the imperative mood in the subject line (more information: http://chris.beams.io/posts/git-commit/#imperative)
	- What is within the quotes is the subject line and the prefix is how it should be read, read the link for a detailed description.
	- Example of good: If applied, this commit will "Refactor subsystem X for readability"
	- Example of bad: If applied, this commit will "Fixed bug with Y"
- Use the body to explain what and why vs. how
- Example of how commit messages have a tendency to deteriorate, https://xkcd.com/1296/
