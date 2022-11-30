import matplotlib.pyplot as plt
import numpy as np


methods = ["Disco(17, 19)", "Searchlight(20)", "Grid(19,19)", "Torus(14,14)", "U-Connect(13)"]
avg_ndt = [96, 71, 102, 63, 61]
max_ndt = [321, 339, 322, 237, 220]

x = np.arange(len(methods))  # the label locations
width = 0.35  # the width of the bars

fig, ax = plt.subplots()
rects1 = ax.bar(x - width/2, avg_ndt, width, label='NDT médio')
rects2 = ax.bar(x + width/2, max_ndt, width, label='NDT máximo')

# Add some text for labels, title and custom x-axis tick labels, etc.
ax.set_ylabel('NDT')
ax.set_title('Disco(17, 19) vs outros métodos')
ax.set_xticks(x, methods)
ax.legend()

ax.bar_label(rects1, padding=3)
ax.bar_label(rects2, padding=3)

fig.tight_layout()

outputImageFileName = "Disco(17,19)-Graph"
plt.savefig(outputImageFileName, bbox_inches='tight')