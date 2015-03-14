### A bone to pick with Arduino ###

"Arduino" is not a programming language. When you write code for Arduino, you are writing C++. But the official Arduino IDE does some magic to your code in the background using a ton of regular expressions. This causes a huge problem, I will show you with an example:

If you use a AVR-G++ to compile a C++ file with

```

void foo()
{
	moo();
}

void moo()
{
	foo();
}

void setup()
{
	moo();
}

void loop()
{
	foo();
}

```

you will receive an error saying "moo was not declared in this scope"

Arduino also uses AVR-G++ to do the compiling, it also adds in the "core" while compiling, this is not such a big deal.

However, if you compile the same code using Arduino, everything is fine, why? Because Arduino secretly added the following line to the top of your file:

`void foo();void moo();void setup();void loop();`

Arduino secretly generates function prototypes for you using regular expressions.

Why is this bad? Well if you want to copy Arduino code to another C++ project, it might not compile. Arduino's background magic makes what is supposed to be perfectly valid C++ code into something that is not valid and thus breaking code portability.

Arduino could get rid of this background magic and I will like it again, but they can't, because then a ton of pre-existing Arduino code will not work.

There's a lot more magic going on with Arduino code, AVR Project IDE has replicated all of these magic spells.