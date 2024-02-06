# Use an official Python runtime as a base image
FROM python:3.8-slim

# Set the working directory in the container
WORKDIR /usr/src/app

# Copy the current directory contents into the container at /usr/src/app
COPY . .

# Install pika package (RabbitMQ client)
RUN pip install --no-cache-dir pika

# Make port 5672 available to the world outside this container
EXPOSE 5672

# Run the script
CMD ["python", "./order.py"]
