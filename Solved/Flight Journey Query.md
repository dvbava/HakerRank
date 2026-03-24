# Flight Journey Query - PostgreSQL

## Problem
Find the shortest travel time (in minutes) from one city to another with at most one plane change.

## Schema
```sql
flights (start_time timestamp, end_time timestamp, start_port char(3), end_port char(3))
airports (city_name varchar(17), port_code char(3))
```

## Solution

### Generic Query (PostgreSQL)
```sql
WITH origin_airports AS (
    SELECT port_code FROM airports WHERE city_name = :origin_city
),
destination_airports AS (
    SELECT port_code FROM airports WHERE city_name = :destination_city
),
direct AS (
    SELECT
        EXTRACT(EPOCH FROM (f.end_time - f.start_time)) / 60 AS duration_minutes
    FROM flights f
    WHERE f.start_port IN (SELECT port_code FROM origin_airports)
      AND f.end_port   IN (SELECT port_code FROM destination_airports)
),
one_stop AS (
    SELECT
        EXTRACT(EPOCH FROM (f2.end_time - f1.start_time)) / 60 AS duration_minutes
    FROM flights f1
    JOIN flights f2
      ON f1.end_port = f2.start_port
     AND f2.start_time >= f1.end_time
    WHERE f1.start_port IN (SELECT port_code FROM origin_airports)
      AND f2.end_port   IN (SELECT port_code FROM destination_airports)
      AND f1.end_port   NOT IN (SELECT port_code FROM destination_airports)
)
SELECT MIN(duration_minutes) AS shortest_journey_minutes
FROM (
    SELECT duration_minutes FROM direct
    UNION ALL
    SELECT duration_minutes FROM one_stop
) all_journeys;
```

### Parameters
- `:origin_city` - Starting city name (e.g., 'New York')
- `:destination_city` - Destination city name (e.g., 'Tokyo')

### Key Logic
- **Direct flights**: Origin airport → Destination airport
- **One-stop flights**: Origin → Intermediate → Destination
  - Connection valid if `f2.start_time >= f1.end_time`
  - Excludes routes that land at destination then continue (already counted as direct)
- Returns `NULL` if no valid journey exists

### Example Test Data
```sql
-- Create test tables
CREATE TEMP TABLE flights (
    start_time timestamp NOT NULL,
    end_time timestamp NOT NULL,
    start_port char(3) NOT NULL,
    end_port char(3) NOT NULL
);

CREATE TEMP TABLE airports (
    city_name varchar(17) NOT NULL,
    port_code char(3) NOT NULL
);

-- Insert flight data
INSERT INTO flights VALUES
  ('2023-02-10 10:00', '2023-02-12 12:00', 'JFK', 'NRT'),
  ('2023-01-30 13:00', '2023-01-30 16:00', 'LGA', 'LAX'),
  ('2023-01-30 17:00', '2023-01-31 06:33', 'LAX', 'HND'),
  ('2023-01-30 15:55', '2023-01-31 04:20', 'LAX', 'HND'),
  ('2023-03-03 04:00', '2023-03-03 08:30', 'JFK', 'CDG'),
  ('2023-03-03 08:30', '2023-03-03 10:30', 'CDG', 'MUC'),
  ('2023-03-03 10:40', '2023-03-03 13:30', 'MUC', 'HND');

-- Insert airport data
INSERT INTO airports VALUES
  ('New York', 'JFK'),
  ('New York', 'LGA'),
  ('Paris', 'CDG'),
  ('Tokyo', 'HND'),
  ('Los Angeles', 'LAX'),
  ('Tokyo', 'NRT'),
  ('Munich', 'MUC');

-- Expected result: 1053 minutes (LGA → LAX → HND)
-- JFK → NRT direct: 3000 minutes
-- LGA → LAX → HND (17:00 departure): 1053 minutes ✓
-- LGA → LAX → HND (15:55 departure): Invalid (departs before LGA arrival)
-- JFK → CDG → MUC → HND: Requires 2 stops (excluded)
```

### Notes
- `EXTRACT(EPOCH FROM interval)` returns seconds; divide by 60 for minutes
- Parameter syntax varies by client (`:param`, `$1`, `%(param)s`, etc.)
- Query naturally handles multiple airports per city
