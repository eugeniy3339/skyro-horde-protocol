Rewiev of Diff Example from the task1 (https://excessive-muscari-c40.notion.site/Week-7-AI-in-game-development-3f69ab114ddc80339734fc68818f5d1b)

1. Enemy AI ma byt v Gameplay priecinku, lebo neriesi grafiku, ale funkcionalitu enemaka.
2. Wave spawner. 
3. Combat.inputactions, lebo nemame vytvarat novy inputaction podla ulohy z predosleho tyzdna.
4. Wave spawner by nemal brat zoznam waviek zo sceny, ale z data priecinku. EnemyAI by lezal v gameplay priecinku, nemal by navmesh, lebo ten tam neni potrebny. Combat.inputactions by som nemal. V Arena scene su nejake zbytocne veci, ako 3 vlni napriklad, tie by som tiez nedaval.