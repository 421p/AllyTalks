<?php

namespace AllyTalks\WebsocketCommunicationServer\Client;

use AllyTalks\ORM\Entities\User;
use Ratchet\ConnectionInterface;

class Client
{
    private $connection;
    private $user;

    public function __construct(ConnectionInterface $connection, User $user)
    {
        $this->connection = $connection;
        $this->user = $user;
    }

    public function getConnection() : ConnectionInterface
    {
        return $this->connection;
    }

    public function setConnection(ConnectionInterface $connection)
    {
        $this->connection = $connection;
    }

    public function getUser() : User
    {
        return $this->user;
    }

    public function setUser(User $user)
    {
        $this->user = $user;
    }

    public function getResourceId()
    {
        return $this->connection->resourceId;
    }
}
