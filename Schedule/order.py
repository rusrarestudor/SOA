import pika
import json
import time
from random import choice, randint, uniform
from datetime import datetime, timedelta

# CloudAMQP settings
amqp_url = 'amqps://jyitxztm:t9uvBLzfma8VJfFsk8JwQVTJ9ULNT0q2@turkey.rmq.cloudamqp.com/jyitxztm'  # Replace this with your CloudAMQP AMQP URL

params = pika.URLParameters(amqp_url)
params.socket_timeout = 5

try:
    # Establish connection to CloudAMQP
    connection = pika.BlockingConnection(params)
    channel = connection.channel()

    # Declare the queue (make sure it matches your CloudAMQP settings)
    queue_name = 'soa'
    channel.queue_declare(queue=queue_name, durable=True)  # durable=True if the queue should survive a broker restart

    # List of names for random choice
    names = ['Alice', 'Bob', 'Charlie', 'David', 'Eve']

    # Function to publish a message
    def publish_message():
        name = choice(names)
        hour = randint(10, 18)  # random hour between 10 and 18
        minute = randint(0, 59) 
        appointment_time = (datetime.now().replace(hour=hour, minute=minute, second=0, microsecond=0) + timedelta(hours=hour)).strftime('%I:%M %p')
        message = {'name': name, 'appointment_time': appointment_time}
        channel.basic_publish(exchange='', routing_key=queue_name, body=json.dumps(message), properties=pika.BasicProperties(delivery_mode=2))  # make message persistent
        print(" [x] Sent %r" % message)

    # Continuously publish messages with random intervals around 7 seconds
    while True:
        publish_message()
        time.sleep(uniform(5, 9))  # Sleep for a random time between 5 to 9 seconds, averaging around 7 seconds

except pika.exceptions.AMQPConnectionError as err:
    print("Failed to connect to CloudAMQP server:", err)
    # Add more specific error handling if needed
except Exception as e:
    print("An unexpected error occurred:", e)
finally:
    # This block will execute no matter what
    if 'connection' in locals() and connection.is_open:
        connection.close()
        print("Connection closed.")
