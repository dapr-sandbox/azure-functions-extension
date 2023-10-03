import azure.functions as func

from create_new_order import createNewOrder
from consume_message_from_kafka import consumeMessageFromKafka
from invoke_output_binding import invokeOutputBinding
from print_topic_message import printTopicMessage
from retrieve_order import retrieveOrder
from retrieve_secret import retrieveSecret
from send_message_to_kafka import sendMessageToKafka
from transfer_event_between_topics import transferEventBetweenTopics

app = func.FunctionApp()
app.register_blueprint(createNewOrder)
app.register_blueprint(consumeMessageFromKafka)
app.register_blueprint(invokeOutputBinding)
app.register_blueprint(printTopicMessage)
app.register_blueprint(retrieveOrder)
app.register_blueprint(retrieveSecret)
app.register_blueprint(sendMessageToKafka)
app.register_blueprint(transferEventBetweenTopics)