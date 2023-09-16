#pragma once
#include "worker.h"
class Manager :
    public Worker
{
public:
    Manager(int num, string name, int did);
    void showInfo();
    string getDeptName();
};

