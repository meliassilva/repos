// ConsoleApplication1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <ostream>

using namespace std;
#define NAME_SIZE 50 // Define Macro

class Person {
	int id; // unique identifier
	char name[NAME_SIZE];


public:
	virtual void aboutMe() {
		cout << "I am a person.";
	}
	virtual bool addCourse(string s) = 0;
};

class Student : public Person {
public:
	void aboutMe() {
		cout << "I am a student.";
	}
	bool addCourse(string s) {
		cout << "Added course " << s >> " to student." << endl;
		return true;
	}
};

int main()
{
	Student * p = new Student();
	p->aboutMe(); // prints
	delete p; // delete allocated memory
	return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
