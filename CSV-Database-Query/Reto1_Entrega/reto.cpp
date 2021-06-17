// Agustín Pumarejo Ontañón, A01028997
// Adriana Abella Kuri, A01329591
// Reto 1:

#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
using namespace std;

class Record
{
public:
    string date;
    string time;
    string sourceIP;
    string sourcePort;
    string sourceName;
    string destIP;
    string destPort;
    string destName;

    Record(){};

    Record(string _date, string _time, string _sourceIP, string _sourcePort, string _sourceName, string _destIP, string _destPort, string _destName)
    {
        date = _date;
        time = _time;
        sourceIP = _sourceIP;
        sourcePort = _sourcePort;
        sourceName = _sourceName;
        destIP = _destIP;
        destPort = _destPort;
        destName = _destName;
    }

    void printRecord()
    {
        cout << date << ", " << time << ", " << sourceIP << ", " << sourcePort << ", " << sourceName << ", " << destIP << ", " << destPort << ", " << destName << endl;
    }
};

class RecordManager
{
public:
    vector<Record> entries;
    string order;

    void read(string file)
    {
        ifstream fileIn;
        fileIn.open(file);
        string record, parts;
        vector<string> data;
        while (getline(fileIn, record))
        {
            istringstream sIn(record);
            while (getline(sIn, parts, ','))
            {
                data.push_back(parts);
            }
            if (data[7].find('\r') != data[7].npos)
            {
                data[7] = data[7].substr(0, data[7].size() - 1);
            }
            Record r(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]);
            entries.push_back(r);
            data.clear();
        }
    }

    void sort(vector<Record> &ent, string type)
    {
        order = type;
        sortAux(ent, 0, entries.size() - 1, type);
    }

    void sortAux(vector<Record> &ent, int low, int high, string type)
    {
        if (low >= high)
        {
            return;
        }
        int j = partition(ent, low, high, type);
        sortAux(ent, low, j - 1, type);
        sortAux(ent, j + 1, high, type);
    }

    int partition(vector<Record> &ent, int low, int high, string type)
    {
        int pivote = low;
        int i = low + 1;
        int j = high;
        while (true)
        {
            while (comparator(ent[i], ent[pivote], type) <= 0 && i < high)
            {
                i++;
            }
            while (comparator(ent[j], ent[pivote], type) > 0 && j > low)
            {
                j--;
            }
            if (i >= j)
            {
                break;
            }
            else
            {
                intercambiar(ent, i, j);
            }
        }
        intercambiar(ent, pivote, j);
        return j;
    }

    void intercambiar(vector<Record> &ent, int pos1, int pos2)
    {
        Record t = ent[pos1];
        ent[pos1] = ent[pos2];
        ent[pos2] = t;
    }

    int binarySearch(string target)
    {
        int inicio = 0;
        int fin = entries.size() - 1;
        if ((compareStrVsR(target, entries[fin], order) > 0) || (compareStrVsR(target, entries[inicio], order) < 0))
        {
            return -1;
        }
        while (fin >= inicio)
        {
            int medio = (inicio + fin) / 2;
            if (compareStrVsR(target, entries[medio], order) == 0)
            {
                return medio;
            }
            else if (compareStrVsR(target, entries[medio], order) > 0)
            {
                inicio = medio + 1;
            }
            else
            {
                fin = medio - 1;
            }
        }
        return -1;
    }
    int comparatorAux(string a, string b)
    {
        if (a < b)
        {
            return -1;
        }
        else if (a == b)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    int comparator(Record a, Record b, string type)
    {
        if (type == "date")
        {
            return comparatorAux(a.date, b.date);
        }
        else if (type == "time")
        {
            return comparatorAux(a.time, b.time);
        }
        else if (type == "sourceIP")
        {
            return comparatorAux(a.sourceIP, b.sourceIP);
        }
        else if (type == "sourcePort")
        {
            return comparatorAux(a.sourcePort, b.sourcePort);
        }
        else if (type == "sourceName")
        {
            return comparatorAux(a.sourceName, b.sourceName);
        }
        else if (type == "destIP")
        {
            return comparatorAux(a.destIP, b.destIP);
        }
        else if (type == "destPort")
        {
            return comparatorAux(a.destPort, b.destPort);
        }
        else
        {
            return comparatorAux(a.destName, b.destName);
        }
    }

    int compareStrVsR(string a, Record b, string type)
    {
        if (type == "date")
        {
            return comparatorAux(a, b.date);
        }
        else if (type == "time")
        {
            return comparatorAux(a, b.time);
        }
        else if (type == "sourceIP")
        {
            return comparatorAux(a, b.sourceIP);
        }
        else if (type == "sourcePort")
        {
            return comparatorAux(a, b.sourcePort);
        }
        else if (type == "sourceName")
        {
            return comparatorAux(a, b.sourceName);
        }
        else if (type == "destIP")
        {
            return comparatorAux(a, b.destIP);
        }
        else if (type == "destPort")
        {
            return comparatorAux(a, b.destPort);
        }
        else
        {
            return comparatorAux(a, b.destName);
        }
    }

    void countRecords(string target, string type, bool print)
    {
        if (type != order)
        {
            sort(entries, type);
        }
        int index = binarySearch(target);
        int lowIndex;
        int highIndex;
        int temp = index;

        if(index == -1)
        {
            cout << "Número de récords con: " << target << " = " << 0 << endl;
        }
        else
        {
            while (compareStrVsR(target, entries[temp], order) == 0 && temp < entries.size())
            {
                highIndex = temp;
                temp++;
            }
            temp = index;
            while (compareStrVsR(target, entries[temp], order) == 0 && temp > 0)
            {
                lowIndex = temp;
                temp--;
            }
            if(print == true)
            {
                for (int i = lowIndex; i <= highIndex; i++)
                {
                    entries[i].printRecord();
                }
            }
            cout << "Número de récords con: " << target << " = " << highIndex - lowIndex + 1 << endl;
        } 
    }

    void printDays(){
        if(order != "date")
        {
            sort(entries, "date");
        }
        string target = entries[0].date;
        cout << target << endl;
        for(int i = 0; i < entries.size()-1; i++)
        {
            if(target != entries[i+1].date)
            {
                target = entries[i+1].date;
                cout << target << endl;
            }
        }
    }
    void printDestPort(){
        if(order != "destPort")
        {
            sort(entries, "destPort");
        }
        string target = entries[0].destPort;
        cout << target << endl;
        for(int i = 0; i < entries.size()-1; i++)
        {
            if(target != entries[i+1].destPort)
            {
                target = entries[i+1].destPort;
                cout << target << endl;
            }
        }
    }
};

int main()
{

    RecordManager equipo11;
    equipo11.read("equipo 11.csv");
    cout << "Datos leidos" << endl;
    
    cout << "Pregunta 1:" << endl;
    cout << "El número de registros es: " << equipo11.entries.size() << endl;

    cout << "Pregunta 2:" << endl;
    equipo11.printDays();
    equipo11.countRecords("11-8-2020", "date", false);

    cout << "Pregunta 3:" << endl;
    equipo11.countRecords("jeffrey.reto.com", "sourceName", false);
    equipo11.countRecords("betty.reto.com", "sourceName", false);
    equipo11.countRecords("katherine.reto.com", "sourceName", false);
    equipo11.countRecords("scott.reto.com", "sourceName", false);
    equipo11.countRecords("benjamin.reto.com", "sourceName", false);
    equipo11.countRecords("samuel.reto.com", "sourceName", false);
    equipo11.countRecords("raymond.reto.com", "sourceName", false);

    cout << "Pregunta 5:" << endl;
    equipo11.countRecords("server.reto.com", "sourceName", false);

    cout << "Pregunta 6:" << endl;
    equipo11.countRecords("gmail.com", "destName", false);
    equipo11.countRecords("outlook.com", "destName", false);
    equipo11.countRecords("protonmail.com", "destName", false);
    equipo11.countRecords("freemailserver.com", "destName", false);

    cout << "Pregunta 7:" << endl;
    equipo11.printDestPort();

    return 0;
}