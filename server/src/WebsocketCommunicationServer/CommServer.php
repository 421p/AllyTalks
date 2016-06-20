<?php

namespace AllyTalks\WebsocketCommunicationServer;

use AllyTalks\WebApp\Model;
use AllyTalks\WebsocketCommunicationServer\Client\Client;
use Ratchet\ConnectionInterface;
use Ratchet\MessageComponentInterface;

class CommServer implements MessageComponentInterface
{

    private $clients = [];
    private $randomConnections = [];
    private $model;

    public function __construct()
    {
        $this->model = new Model();
    }

    function onOpen(ConnectionInterface $conn)
    {
        $this->randomConnections[$conn->getId()] = $conn;
    }

    function onClose(ConnectionInterface $conn)
    {
        unset($this->randomConnections[$conn->resourceId]);
    }

    function onError(ConnectionInterface $conn, \Exception $e)
    {
        // TODO: Implement onError() method.
    }

    function onMessage(ConnectionInterface $from, $msg)
    {
        foreach ($this->randomConnections as $conn) {
            $conn->send($msg);
        }
    }
}