import asyncio
import simplejson
import threading
import websockets
import random
from functools import partial


# This function now generates random data instead of consuming from Kafka
async def run_consumer(shutdown_flag, clients, lock):
    print("Starting Random Data Generator.")

    while not shutdown_flag.is_set():
        await asyncio.sleep(5)

        value = {
            "bloodType": random.choice(["A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"]),
            "quantity": random.randint(1, 5)  # in units
        }
        formatted = simplejson.dumps(value)
        print(f"Sending {formatted} to {clients}")

        with lock:
            websockets.broadcast(clients, formatted)

    print("Closing Random Data Generator")

async def handle_connection(clients, lock, connection, path):
    with lock:
        clients.add(connection)

    await connection.wait_closed()

    with lock:
        clients.remove(connection)

async def main():
    shutdown_flag = threading.Event()
    clients = set()
    lock = threading.Lock()

    asyncio.create_task(run_consumer(shutdown_flag, clients, lock))

    print("Starting WebSocket Server.")
    try:
        async with websockets.serve(partial(handle_connection, clients, lock),
                                    "localhost", 8080):
            await asyncio.Future()
    finally:
        shutdown_flag.set()

asyncio.run(main())
