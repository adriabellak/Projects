// Agustín Pumarejo Ontañón, A01028997
// Adriana Abella Kuri, A01329591
// Reto 2

#include <iostream>
#include <stack>
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
            Record r(data[0], timeFixer(data[1]), data[2], data[3], data[4], data[5], data[6], data[7]);
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
            if (comparatorAux(a.date, b.date) == 0)
            {
                return comparatorAux(a.time, b.time);
            }
            else
            {
                return comparatorAux(a.date, b.date);
            }
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

    stack<Record> returnRecords(string target, string type, bool print)
    {
        if (type != order)
        {
            sort(entries, type);
        }
        int index = binarySearch(target);
        int lowIndex;
        int highIndex;
        int temp = index;
        stack<Record> conexiones;

        if (index == -1)
        {
            return conexiones;
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
            RecordManager temp;
            for (int i = lowIndex; i <= highIndex; i++)
            {
                temp.entries.push_back(entries[i]);
            }
            temp.sort(temp.entries, "date");
            if (print == true)
            {
                for (int i = 0; i < temp.entries.size(); i++)
                {
                    temp.entries[i].printRecord();
                }
            }
            int c = 0;
            for (int i = 0; i < temp.entries.size(); i++)
            {
                c++;
                conexiones.push(temp.entries[i]);
            }
            if (c > 50)
            {
                cout << "son muchos jaja: " << c << endl;
            }
            else
            {
                cout << "son poquis jsjja: " << c << endl;
            }
            return conexiones;
        }
    }
    string timeFixer(string badTime)
    {
        string goodTime;
        int pos;
        pos = badTime.find(":");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime = badTime.substr(0, 3);
        badTime = badTime.substr(3, string::npos);
        pos = badTime.find(":");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime += badTime.substr(0, 3);
        badTime = badTime.substr(3, string::npos);
        if (badTime.length() == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime += badTime;
        return goodTime;
    }
};

class ConexionesComputadora
{
public:
    string IP;
    string nombre;
    stack<Record> con_entrantes;
    stack<Record> con_salientes;
    RecordManager all_entries;

    ConexionesComputadora(string num, RecordManager _entries, bool print)
    {
        IP = "172.23.97." + num;
        cout << "IP: " << IP << endl;
        all_entries = _entries;
        cout << "Conexiones entrantes" << endl;
        con_entrantes = all_entries.returnRecords(IP, "destIP", print);
        cout << "Conexiones salientes" << endl;
        con_salientes = all_entries.returnRecords(IP, "sourceIP", print);
        nombre = con_salientes.top().sourceName;
    }

    void eXtrA()
    {
        int c = 0;
        string temp = "";
        cout << "Sitios web a los que se conectó 3 veces seguidas: " << endl;
        for (int i = 0; i < con_salientes.size(); i++)
        {
            if (con_salientes.top().destName == temp)
            {
                c++;
            }
            else
            {
                temp = con_salientes.top().destName;
                c = 0;
            }
            if (c == 3)
            {
                cout << temp << endl;
            }
            con_salientes.pop();
        }
    }

    void printTopEnt()
    {
        cout << "Última conexión entrante: " << endl;
        con_entrantes.top().printRecord();
    }

    void printTopSal()
    {
        cout << "Última conexión saliente: " << endl;
        con_salientes.top().printRecord();
    }
};

int main()
{
    RecordManager equipo11;
    equipo11.read("nuevo11.csv");
    cout << "Datos leidos" << endl;
    string num;
    cout << "Número: ";
    cin >> num;
    // 139
    // para que se impriman todas las conexiones entrantes y salientes, cambiar el tercer parametro a true
    ConexionesComputadora margaret(num, equipo11, false);
    margaret.printTopEnt();
    margaret.printTopSal();
    margaret.eXtrA();

    return 0;
}