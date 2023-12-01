namespace TPI
{
    enum EnumLandType
    {
        wasteland, // sin efecto
        plain, // sin efecto
        forest, // sin efecto
        urbanArea, // sin efecto
        dump, // 5% de chances de dañar componentes del operador -- de acá se sacan los elementos a reciclar
        lake, // k9 y M8 no pueden pasar
        electronicDump, // chances de reducir 20% la carga máxima de la batería
        barrack,   // puede cargar la batería y reparar operadores
        recyclingSite  // pueden cargar batería -- lugar para reciclar desechos
    }
}
