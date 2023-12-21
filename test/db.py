import sqlite3

# Connect to the SQLite database
conn = sqlite3.connect('C:\\Users\\johndohoney\\AppData\\Roaming\\DBeaverData\\workspace6\\.metadata\\sample-database-sqlite-1\\Chinook.db')

# Create a cursor object
c = conn.cursor()

# Execute a SELECT query
c.execute("SELECT * FROM Customer where State is not NULL")

# Fetch all the rows
rows = c.fetchall()

# Display the rows
for row in rows:
    print(row)

# Close the connection
conn.close()