# -*- coding: utf-8 -*-
"""
Created on Thu Nov 23 14:13:11 2023

@author: Andrew Mulcahy
"""


import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

import sys

# for arg in sys.argv:
#     print(arg)

if len(sys.argv) > 1:
    file_path = sys.argv[1].replace('\\', '\\\\')
else:
    file_path = "C:\\Users\\Andrew Mulcahy\\Documents\\Programming\\Data\\NBAPlayerStats2223\\2022-2023 NBA Player Stats - Regular.csv"

data = pd.read_csv(file_path)

'''
diverged here

#print(list(data.get('Tm').drop_duplicates()))
team = "\'" + "DEN" + "\'"
print(data.query(f"Tm == {team}"))

'''
if len(sys.argv) > 2:
    if sys.argv[2] == "GET_TEAMS":
        print((list(data.get('Tm').drop_duplicates())).sort())
    elif sys.argv[2] == "SUMMARISE_TEAM":
        team = sys.argv[3]
        print(data.query(f"Tm == {team}"))


shooters = data.loc[data['PTS'] > 24]
young_shooters = shooters.loc[shooters['Age'] < 28]

colours = np.random.rand(31)
z = list(shooters.get('FGA'))
y = list(shooters.get('PTS'))
n = list(shooters.get("Player"))
plt.scatter(z, y, s=shooters.get('3P%')**4*850, c=colours)
for i, txt in enumerate(n):
    plt.annotate(txt, xy=(z[i], y[i]), fontsize=5)
plt.savefig("graph.svg")
plt.show()
# plt.scatter(young_shooters.get('FGA'), young_shooters.get('PTS'))
# plt.show()

'''
user_selection = input("Enter team initials (help/exit): ")
while not (user_selection == 'exit'):
    if user_selection == 'help':
        print(list(data.get('Tm').drop_duplicates()))
    else:
        print(data.query(f"Tm == \'{user_selection}\'"))
    user_selection = input("Enter team initials (help/exit): ")
'''
# filter_more_than_73_games = data.query('G > 73')


