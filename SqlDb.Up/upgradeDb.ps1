dotnet run -c release --project . -- -c "Server=(localdb)\MSSQLLocalDB; Database=School; Trusted_connection=true" -d $args;