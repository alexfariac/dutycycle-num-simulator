import matplotlib.pyplot as plt

fig, ax = plt.subplots()

#UConnect(13); S2: UConnect(13); lcm: 169; gcf: 169; AVG-NDT: 77; MAX-NDT: 168;
#Searchlight(20); S2: UConnect(13); lcm: 33800; gcf: 1;AVG-NDT: 65; MAX-NDT: 259;
#Disco(17, 19); S2: UConnect(13); lcm: 54587; gcf: 1; AVG-NDT: 61; MAX-NDT: 220;
#Grid(19,19); S2: UConnect(13); lcm: 61009; gcf: 1; AVG-NDT: 80; MAX-NDT: 246;
#Torus(14,14); S2: UConnect(13); lcm: 33124; gcf: 1; AVG-NDT: 59; MAX-NDT: 181;

methods = ["U-Connect(13)", "Searchlight(20)", "Disco(17, 19)", "Grid(19,19)", "Torus(14,14)"]
lcm = [169, 33800, 54587, 61009, 33124]
gcf = [169, 1, 1, 1, 1]
avg_ndt = [77, 65, 61, 80, 59]
max_ndt = [168, 259, 220, 246, 181]

bar_colors = ['tab:red', 'tab:blue', 'tab:blue', 'tab:blue', 'tab:blue']

ax.bar(methods, max_ndt, label=methods, color=bar_colors)

ax.set_ylabel('NDT máximo (Slots)')
ax.set_title('U-Connect(13)')
ax.legend(title='Método comparado')

outputImageFileName = "U-Connect(13)-MAX-NDT-Graph"
plt.savefig(outputImageFileName, bbox_inches='tight')