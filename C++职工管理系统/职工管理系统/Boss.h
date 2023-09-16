#pragma once
#include "worker.h"

class Boss :
    public Worker
{
public:
    Boss(int num, string name, int did);
    void showInfo();
    string getDeptName();
};

