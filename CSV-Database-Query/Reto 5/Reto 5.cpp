// Agustín Pumarejo Ontañón, A01028997
// Adriana Abella Kuri, A01329591
// Reto 5

#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include <unordered_map>
#include <stack>
#include <queue>

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
            Record r(dateFixer(data[0]), timeFixer(data[1]), data[2], data[3], data[4], data[5], data[6], data[7]);
            entries.push_back(r);
            data.clear();
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

    string dateFixer(string badTime)
    {
        string goodTime;
        int pos;
        pos = badTime.find("-");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime = badTime.substr(0, 3);
        badTime = badTime.substr(3, string::npos);
        pos = badTime.find("-");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime += badTime;
        return goodTime;
    }
};

template <class T>
class Nodo
{
public:
    T valor;
    bool visitado;
    unordered_map<T, Nodo<T> *> adyacencia;

    Nodo()
    {
        valor = NULL;
        visitado = false;
    }

    Nodo(T _valor)
    {
        valor = _valor;
        visitado = false;
    }
};

template <class T>
class Grafo
{
public:
    unordered_map<T, Nodo<T> *> nodos;

    Grafo() {}

    void insert(T valor)
    {
        Nodo<T> *nuevo = new Nodo<T>(valor);
        nodos[valor] = nuevo;
    }

    void conexion(T nodo1, T nodo2)
    {
        nodos[nodo1]->adyacencia[nodo2] = nodos[nodo2];
        nodos[nodo2]->adyacencia[nodo1] = nodos[nodo1];
    }

    void conexion_dirigida(T nodo1, T nodo2)
    {
        nodos[nodo1]->adyacencia[nodo2] = nodos[nodo2];
    }

    bool BFS(T nodo1, T nodo2)
    {
        visitados_falso();
        queue<Nodo<T> *> cola;
        cola.push(nodos[nodo1]);
        Nodo<T> *temp;
        while (cola.empty() == false)
        {
            if (cola.front()->valor == nodo2)
            {
                return true;
            }
            else
            {
                cola.front()->visitado = true;
                temp = cola.front();
                cola.pop();
                for (pair<T, Nodo<T> *> it : temp->adyacencia)
                {
                    if (it.second->visitado == false)
                    {
                        cola.push(it.second);
                    }
                }
            }
        }
        return false;
    }

    bool DFS(T nodo1, T nodo2)
    {
        visitados_falso();
        stack<Nodo<T> *> pila;
        pila.push(nodos[nodo1]);
        Nodo<T> *temp;
        while (pila.empty() == false)
        {
            if (pila.top()->valor == nodo2)
            {
                return true;
            }
            else
            {
                pila.top()->visitado = true;
                temp = pila.top();
                pila.pop();
                for (pair<T, Nodo<T> *> it : temp->adyacencia)
                {
                    if (it.second->visitado == false)
                    {
                        pila.push(it.second);
                    }
                }
            }
        }
        return false;
    }

    void visitados_falso()
    {
        for (pair<T, Nodo<T> *> it : nodos)
        {
            it.second->visitado = false;
        }
    }

    void grafoInterno(RecordManager vertices)
    {
        for (Record it : vertices.entries)
        {
            if ((it.destIP.find("172.23.97.") != string::npos) && (it.sourceIP.find("172.23.97.") != string::npos))
            {
                if (nodos.find(it.sourceIP) == nodos.end())
                {
                    insert(it.sourceIP);
                }
                if (nodos.find(it.destIP) == nodos.end())
                {
                    insert(it.destIP);
                }
                conexion_dirigida(it.sourceIP, it.destIP);
            }
        }
    }

    void adyacencias()
    {
        for (pair<T, Nodo<T> *> it : nodos)
        {
            cout << it.first << ": " << it.second->adyacencia.size() << endl;
        }
    }

    int contarConexiones(T nodo)
    {
        int contador = 0;
        for (pair<T, Nodo<T> *> it : nodos)
        {
            if (it.second->adyacencia.find(nodo) != it.second->adyacencia.end())
            {
                contador++;
            }
        }
        return contador;
    }

    void grafoTodas(RecordManager vertices)
    {
        for (Record it : vertices.entries)
        {
            if (nodos.find(it.sourceIP) == nodos.end())
            {
                insert(it.sourceIP);
            }
            if (nodos.find(it.destIP) == nodos.end())
            {
                insert(it.destIP);
            }
            conexion_dirigida(it.sourceIP, it.destIP);
        }
    }

    void grafoDia(RecordManager vertices, string dia)
    {
        for (Record it : vertices.entries)
        {
            if (it.date == dia)
            {
                if (nodos.find(it.sourceIP) == nodos.end())
                {
                    insert(it.sourceIP);
                }
                if (nodos.find(it.destIP) == nodos.end())
                {
                    insert(it.destIP);
                }
                conexion_dirigida(it.sourceIP, it.destIP);
            }
        }
    }
};

int main()
{

    RecordManager equipo11;
    equipo11.read("nuevo11.csv");
    Grafo<string> conexionesInternas;
    string A = "172.23.97.22", B = "213.193.6.183", C = "215.109.221.174";
    cout << "A: " << A << endl
         << "B: " << B << endl
         << "C: " << C << endl
         << endl;
    conexionesInternas.grafoInterno(equipo11);
    cout << "Pregunta 1" << endl
         << "Computadoras internas a las que A se conecta: " << conexionesInternas.nodos[A]->adyacencia.size() << endl
         << endl;
    cout << "Todas las conexiones hacia la red interna" << endl;
    conexionesInternas.adyacencias();
    cout << endl;
    cout << "Pregunta 2" << endl
         << "Computadoras internas que se conectan a A: " << conexionesInternas.contarConexiones(A) << endl
         << endl;

    Grafo<string> conexiones;
    conexiones.grafoTodas(equipo11);
    cout << "Pregunta 3" << endl
         << "Computadoras que se conectan a B: " << conexiones.contarConexiones(B) << endl
         << endl;

    cout << "Pregunta 4" << endl
         << "Computadoras que se conectan a C (por día) " << endl;

    vector<Grafo<string>> dias;
    for (int i = 10; i < 22; i++)
    {
        Grafo<string> *nuevo = new Grafo<string>();
        nuevo->grafoDia(equipo11, to_string(i) + "-08-2020");
        dias.push_back(*nuevo);
        cout << to_string(i) + "-08-2020: " << nuevo->contarConexiones(C) << endl;
    }
    return 0;
}