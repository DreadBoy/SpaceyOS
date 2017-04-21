# SpaceyOS

Here you can find my latest prototype of space ship OS. Main goal behind this project is to create simple terminal-based OS, that allows you to command and automate your own space ship. 

## Prominent features

* access to basic information about ship and it's systems/components
* access to ship's file system; copy, move, create and edit files and directories
* ability to compile all *.csx files in file system
* ability to attach compiled snippets to ship's components, hence automating it

## Current state of project

If you clone and run SpaceyOS project, the terminal will pop up, prompting you to type. All mentioned features are fully implemented, try them out! Type `help` to see all available commands or `test` to simulate space ship being hit. Your space ship currently has only 1 system, namely ForceField, you should be able to attach some scripts to it. Commands below will help you along the way.

```
touch FrontShield.csx
nano FrontShield.csx // this should open you default text editor
// copy and paste contets of /snipps/FrontShield.csx
compile
system comps //get id of installed comp (space slang for ship's component)
comp 45d1 attach FrontShield //replace 45d1 with id of your comp
comp 45d1 // check if snipp was installed (snipp is space slang for code snippet)
test // make your ship being hit by laser, your snipp will try to change frequency of force field
test // force field with new frequency blocks next laser hit
exit // to close the terminal
```

I used a lot of interfaces to make everything type-safe and I managed to avoid using reflections. It doesn't make a lof of difference for end user but allows compile time type-safety which is cool.