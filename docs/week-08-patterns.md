1. Vlna beží a hráč stlačí Pause. Aký je stav a kde ostanú nepriatelia?
	GameState sa zmeny na pause a enemaci sa prestanu spawnovat a zastavia sa na tej pozicii, na ktorej boli pocas pauznutia hry.
2. Vlna je v pauze a nepriateľ sa dotkne hráča. Aký je stav?
	Nic sa nestane a bude sa cakat, kym sa hra neunpauzne. Po unpauznuti sa stane event OnTriggerEnter u enemaka.
3. Hráč je v GameOver a stlačí Fire. Čo spraví Submit ?
	Submit returne uplne na zaciatku po checku terajseho stavu.
4. Jednou vetou: čo z náčrtu GameManager dnes nesmie skončiť v jednej triede, a ktorá z vašich tried to bude držať namiesto nej?
	Movement, Shooting, Wave manager a Inputs manager.