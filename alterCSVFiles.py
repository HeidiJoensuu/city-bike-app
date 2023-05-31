import os
import csv

file05 = os.path.join("databaseFiles", "2021-05.csv")
file06 = os.path.join("databaseFiles", "2021-06.csv")
file07 = os.path.join("databaseFiles", "2021-07.csv")
file05E = os.path.join("databaseFiles", "2021-05-edit.csv")
file06E = os.path.join("databaseFiles", "2021-06-edit.csv")
file07E = os.path.join("databaseFiles", "2021-07-edit.csv")


if os.path.isfile(file05):
  with open(file05) as previousFile, open(file05E, "w", newline='') as newFile:
    reader = csv.reader(previousFile, delimiter=',', quotechar='"')
    writer = csv.writer(newFile, delimiter=",", quotechar='"')
    headers = next(reader)
    for index, heading in enumerate(headers):
      if heading == 'Return':
          headers[index] = 'ReturnTime'
    
    print("Rewriting 2021-05 file")
    writer.writerow(headers)
    for row in reader:
       #some distances are float numbers
       if "." in row[6]:
          row[6] = row[6][:-1]
          row[6] = row[6][:-1]
          row[6] = row[6][:-1]
       writer.writerow(row)

if os.path.isfile(file06):
  with open(file06) as previousFile, open(file06E, "w", newline='') as newFile:
    reader = csv.reader(previousFile, delimiter=',', quotechar='"')
    writer = csv.writer(newFile, delimiter=",", quotechar='"')
    headers = next(reader)
    for index, heading in enumerate(headers):
      if heading == 'Return':
          headers[index] = 'ReturnTime'
    
    print("Rewriting 2021-06 file")
    writer.writerow(headers)
    for row in reader:
       if "." in row[6]:
          row[6] = row[6][:-1]
          row[6] = row[6][:-1]
          row[6] = row[6][:-1]
       writer.writerow(row)

if os.path.isfile(file07):
  with open(file07) as previousFile, open(file07E, "w", newline='') as newFile:
    reader = csv.reader(previousFile, delimiter=',', quotechar='"')
    writer = csv.writer(newFile, delimiter=",", quotechar='"')
    headers = next(reader)
    for index, heading in enumerate(headers):
      if heading == 'Return':
          headers[index] = 'ReturnTime'
    
    print("Rewriting 2021-07 file")
    writer.writerow(headers)
    for row in reader:
       if "." in row[6]:
          row[6] = row[6][:-1]
          row[6] = row[6][:-1]
          row[6] = row[6][:-1]
       writer.writerow(row)

print("done")




