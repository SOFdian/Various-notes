#pragma once
#include<iostream>
#include<string>
using namespace std;

class Worker {
public:
	virtual void showInfo() = 0;
	virtual string getDeptName() = 0;

public:
	int number = 0;
	string name;
	int did;

};
