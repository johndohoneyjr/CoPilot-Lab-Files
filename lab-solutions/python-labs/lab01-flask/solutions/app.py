# Generate APIFlask app
# Add a GET method called "SayHello" that accepts input as a query string using a string as a parameter for the name to say a greeting
import apiflask as api
from apiflask import Schema, abort
from apiflask.fields import Integer, String
from apiflask.validators import Length, OneOf

app = api.APIFlask(__name__)

# Schema for input
class SayHelloIn(Schema):
    name = String(required=True, validate=Length(0, 10))
    
# Schema for output
class SayHelloOut(Schema):
    message = String()
    
# GET method - Use Query string parameters
@app.get('/SayHello')
@app.input(SayHelloIn, location='query')
@app.output(SayHelloOut)
def say_hello(query_data):
    return {'message': 'Hello ' + query_data['name'] + '!'}

# GET method - Add two query string parameters and return the sum
class AddTwoNumbersIn(Schema):
    num1 = Integer(required=True)
    num2 = Integer(required=True)

class AddTwoNumbersOut(Schema):
    sum = Integer()

@app.get('/AddTwoNumbers')
@app.input(AddTwoNumbersIn, location='query')
@app.output(AddTwoNumbersOut)
def add(query_data):
    return {'sum': query_data['num1'] + query_data['num2']}