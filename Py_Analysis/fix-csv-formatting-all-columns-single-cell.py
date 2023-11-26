# -*- coding: utf-8 -*-
"""
Created on Thu Nov 23 14:13:11 2023

@author: Andrew Mulcahy
"""

file_path = "C:\\Users\\Andrew Mulcahy\\Documents\\Programming\\Data\\NBAPlayerStats2223\\2022-2023 NBA Player Stats - Playoffs - Copy.csv"

import pandas as pd

data = pd.read_csv(file_path)

headings = data.columns.values[0].split(';')

## THIS PART WORKS
# commenting as it only needs to be ran once
# uncomment for final version

for name in headings:
    data[name] = ''
# shift headings left by 1 cell
data.columns.values[0:len(headings)] = headings[0:len(headings)+1]
# delete farthest right (extra) column
new_headings = data.columns.values
data.drop(labels=new_headings[-1], inplace=True, axis=1)

# looping through rows
# check type
# split each string in the first cell into a list
# this is a list of what should be in each cell

for i in range(0, len(data.index)):
    if isinstance(data.loc[i][0], str):
        player_data = data.loc[i][0].split(';')
        
        print(new_headings)
        for j in range(len(new_headings) - 1):
            print(f'j = {j}')
            print(f'new_headings[{j}] = {new_headings[j]}')
            print(f'player_data[{j}] = {player_data[j]}')
            # data.loc[i, new_headings[j]] = player_data[j]


data.to_csv(file_path, index=False)
