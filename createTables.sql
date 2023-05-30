CREATE TABLE IF NOT EXISTS kaupunkiPyoraAsemat (
  FID INT PRIMARY KEY NOT NULL,
  ID INT NOT NULL,
  Nimi VARCHAR ( 250 ) NOT NULL,
  Namn VARCHAR ( 250 ) NOT NULL,
  Name VARCHAR ( 250 ) NOT NULL,
  Osoite VARCHAR ( 250 ) NOT NULL,
  Adress VARCHAR ( 250 ) NOT NULL,
  Kaupunki VARCHAR ( 250 ),
  Stad VARCHAR ( 250 ),
  Operaattor VARCHAR ( 250 ),
  Kapasiteet INT NOT NULL,
  x float8 NOT NULL,
  y float8 NOT NULL
);

CREATE TABLE IF NOT EXISTS May (
  Departure timestamp NOT NULL,
  ReturnTime timestamp NOT NULL,
  Departure_station_id INT NOT NULL,
  Departure_station_name VARCHAR ( 250 ) NOT NULL,
  Return_station_id INT NOT NULL,
  Return_station_name VARCHAR ( 250 )  NOT NULL,
  Covered_distance_m INT,
  Duration_sec INT
);

CREATE TABLE IF NOT EXISTS June (
  Departure timestamp NOT NULL,
  ReturnTime timestamp NOT NULL,
  Departure_station_id INT NOT NULL,
  Departure_station_name VARCHAR ( 250 ) NOT NULL,
  Return_station_id INT NOT NULL,
  Return_station_name VARCHAR ( 250 )  NOT NULL,
  Covered_distance_m INT,
  Duration_sec INT
);

CREATE TABLE IF NOT EXISTS July (
  Departure timestamp NOT NULL,
  ReturnTime timestamp NOT NULL,
  Departure_station_id INT NOT NULL,
  Departure_station_name VARCHAR ( 250 ) NOT NULL,
  Return_station_id INT NOT NULL,
  Return_station_name VARCHAR ( 250 )  NOT NULL,
  Covered_distance_m INT,
  Duration_sec INT
);
COPY kaupunkiPyoraAsemat from '/csv/pyoraAsemat.csv' CSV HEADER;
COPY May from '/csv/2021-05.csv' CSV HEADER;
COPY June from '/csv/2021-06.csv' CSV HEADER;
COPY July from '/csv/2021-07.csv' CSV HEADER;

DELETE FROM May WHERE covered_distance_m < 10;
DELETE FROM June WHERE covered_distance_m < 10;
DELETE FROM July WHERE covered_distance_m < 10;

DELETE FROM may a USING (
  SELECT MIN(ctid) as ctid, departure, returnTime, departure_station_id, return_station_id, covered_distance_m, duration_sec 
  FROM may 
  GROUP BY departure, returnTime, departure_station_id, return_station_id, covered_distance_m, duration_sec 
  HAVING COUNT(*) >1
) b
where a.departure = b.departure
and a.returnTime = b.returnTime
and a.departure_station_id = b.departure_station_id
and a.return_station_id = b.return_station_id
and a.covered_distance_m = b.covered_distance_m
and a.duration_sec = b.duration_sec
AND a.ctid <> b.ctid;

DELETE FROM june a USING (
  SELECT MIN(ctid) as ctid, departure, returnTime, departure_station_id, return_station_id, covered_distance_m, duration_sec 
  FROM may 
  GROUP BY departure, returnTime, departure_station_id, return_station_id, covered_distance_m, duration_sec 
  HAVING COUNT(*) >1
) b
where a.departure = b.departure
and a.returnTime = b.returnTime
and a.departure_station_id = b.departure_station_id
and a.return_station_id = b.return_station_id
and a.covered_distance_m = b.covered_distance_m
and a.duration_sec = b.duration_sec
AND a.ctid <> b.ctid;

DELETE FROM july a USING (
  SELECT MIN(ctid) as ctid, departure, returnTime, departure_station_id, return_station_id, covered_distance_m, duration_sec 
  FROM may 
  GROUP BY departure, returnTime, departure_station_id, return_station_id, covered_distance_m, duration_sec 
  HAVING COUNT(*) >1
) b
where a.departure = b.departure
and a.returnTime = b.returnTime
and a.departure_station_id = b.departure_station_id
and a.return_station_id = b.return_station_id
and a.covered_distance_m = b.covered_distance_m
and a.duration_sec = b.duration_sec
AND a.ctid <> b.ctid;

ALTER TABLE May ADD COLUMN id SERIAL PRIMARY KEY;
ALTER TABLE June ADD COLUMN id SERIAL PRIMARY KEY;
ALTER TABLE July ADD COLUMN id SERIAL PRIMARY KEY;