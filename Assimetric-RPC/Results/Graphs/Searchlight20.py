import matplotlib.pyplot as plt

fig, ax = plt.subplots()

#S1: Searchlight(20); S2: Searchlight(20); lcm: 200; gcf: 200; DC1: 10; DC2: 10; AVG-NDT: 87; MAX-NDT: 199; ITERATIONS: 20100
#S1: Searchlight(20); S2: Disco(17, 19); lcm: 64600; gcf: 1; DC1: 10; DC2: 10.836; AVG-NDT: 71; MAX-NDT: 339; ITERATIONS: 2086612300
#S1: Searchlight(20); S2: Grid(19,19); lcm: 72200; gcf: 1; DC1: 10; DC2: 10.249; AVG-NDT: 95; MAX-NDT: 367; ITERATIONS: 2_606_456_100
#S1: Searchlight(20); S2: Torus(14,14); lcm: 9800; gcf: 4; DC1: 10; DC2: 10.714; AVG-NDT: 109; MAX-NDT: 662; ITERATIONS: 48_024_900
#S1: Searchlight(20); S2: UConnect(13); lcm: 33800; gcf: 1; DC1: 10; DC2: 11.243; AVG-NDT: 65; MAX-NDT: 259; ITERATIONS: 571236900

methods = ["Searchlight(20)", "Disco(17, 19)", "Grid(19,19)", "Torus(14,14)", "U-Connect(13)"]
lcm = [200, 64600, 72200, 9800, 33800]
gcf = [200, 1, 1, 4, 1]
avg_ndt = [87, 71, 95, 109, 65]
max_ndt = [199, 339, 367, 662, 259]

bar_colors = ['tab:red', 'tab:blue', 'tab:blue', 'tab:blue', 'tab:blue']

ax.bar(methods, max_ndt, label=methods, color=bar_colors)

ax.set_ylabel('NDT máximo (Slots)')
ax.set_title('Searchlight(20)')
ax.legend(title='Método comparado')

outputImageFileName = "Searchlight(20)-MAX-NDT-Graph"
plt.savefig(outputImageFileName, bbox_inches='tight')