import sqlite3

# Connect to the SQLite database
#conn = sqlite3.connect('/workspaces/CoPilot-Lab-Files/db/Chinook.db')
conn = sqlite3.connect('C:\\Users\\johndohoney\\CoPilot-Lab-Files\\db\\Chinook.db')

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