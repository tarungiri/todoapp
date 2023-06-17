# Base SQL Server Docker Image
FROM mcr.microsoft.com/mssql/server

# Set environment variables for the SQL Server instance
ENV SA_PASSWORD=Welcome@1991
ENV ACCEPT_EULA=Y

# Create a directory for database files
RUN mkdir -p /var/opt/mssql/backup

# Copy the .bak file to the container
COPY tododb.bak /var/opt/mssql/backup

# Restore the database during the image build process
RUN /opt/mssql/bin/sqlservr & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome@1991 -Q "RESTORE DATABASE tododb FROM DISK='/var/opt/mssql/backup/tododb.bak' WITH MOVE 'tododb' TO '/var/opt/mssql/data/tododb.mdf', MOVE 'tododb_log' TO '/var/opt/mssql/data/tododb_log.ldf'" \
    && pkill sqlservr

# Expose SQL Server default port
EXPOSE 1433
