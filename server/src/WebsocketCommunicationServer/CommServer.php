<?php

namespace AllyTalks\WebsocketCommunicationServer;

use AllyTalks\WebApp\Model;
use AllyTalks\WebsocketCommunicationServer\Client\Client;
use AllyTalks\WebsocketCommunicationServer\Router\Router;
use Ratchet\ConnectionInterface;
use Ratchet\MessageComponentInterface;
use YaLinqo\Enumerable;

class CommServer implements MessageComponentInterface
{
    /** @var Client[] */
    private $clients = [];
    private $randomConnections = [];
    private $model;
    private $router;

    public function __construct()
    {
        $this->model = new Model();
        $this->router = new Router($this);
    }

    public function onOpen(ConnectionInterface $conn)
    {
        $this->randomConnections[$conn->resourceId] = $conn;
    }

    public function onClose(ConnectionInterface $conn)
    {
        if (array_key_exists($conn->resourceId, $this->randomConnections)) {
            unset($this->randomConnections[$conn->resourceId]);
        }
        
        $client = Enumerable::from($this->clients)->where(function(Client $x) use ($conn) {
            return $x->getResourceId() === $conn->resourceId;
        })->firstOrDefault();

        if ($client) {
            unset($this->clients[array_search($client, $this->clients)]);
        }

    }

    public function onError(ConnectionInterface $conn, \Exception $e)
    {
        $conn->send(json_encode([
            'type' => 'error',
            'sender' => 'service',
            'message' => $e->getMessage(),
        ]));
        echo $e->getMessage().PHP_EOL;
    }

    public function onMessage(ConnectionInterface $from, $msg)
    {
        $message = json_decode($msg, true);

        if ($message['receiver'] === 'service') {
            $this->router->processServiceMessage($from, $message);
        } else {
            $this->router->processRegularMessage($from, $message);
        }
    }

    public function getModel() : Model
    {
        return $this->model;
    }

    public function addClient(Client $client)
    {
        foreach ($this->clients as $i => $cli) {
            if ($client->getResourceId() === $cli->getResourceId()) {
                unset($this->clients[$i]);
            }
        }

        $this->clients[] = $client;
        unset($this->randomConnections[$client->getConnection()->resourceId]);
    }

    public function getClients() : array
    {
        return $this->clients;
    }

    public function disconnectClient(Client $client)
    {
        if (!in_array($client, $this->clients)) {
            throw new \RuntimeException('No client found.');
        }

        foreach ($this->clients as $i => $cli) {
            if ($client->getResourceId() === $cli->getResourceId()) {
                unset($this->clients[$i]);
            }
        }
    }
}
