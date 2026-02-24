# Flow description
1. User uploads file to MinIO
2. We create a new entry in RabbitMQ
3. SaveFileProcessing job reads this entry
   1. Find object with dungeon locator, yield its bytes
   2. Create dungeon entry